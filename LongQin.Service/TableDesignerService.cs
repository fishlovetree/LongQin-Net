using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongQin.Models;
using LongQin.Common;

namespace LongQin.Service
{
    public class TableDesignerService : ServiceBase, ITableDesignerService
    {
        ITableDesignerRepository _tableDesignerRepository = AutofacRepository.Resolve<ITableDesignerRepository>();

        public TableDesignerService()
        {
            base.AddDisposableObject(_tableDesignerRepository);
        }

        public TableDesigner GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _tableDesignerRepository.GetByIdAsync(id);
        }

        public PageModel<TableDesigner> GetListAsync(int pageIndex, int pageSize, int organizationId)
        {
            return _tableDesignerRepository.GetListAsync(pageIndex, pageSize, organizationId);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _tableDesignerRepository.DeleteAsync(id);
        }

        public int InsertAsync(TableDesigner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.TableName))
            {
                throw new ArgumentNullException("TableName错误");
            }

            return _tableDesignerRepository.InsertAsync(model);
        }

        public bool UpdateAsync(TableDesigner model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model不能为null");
            }

            if (string.IsNullOrEmpty(model.TableName))
            {
                throw new ArgumentNullException("TableName错误");
            }

            return _tableDesignerRepository.UpdateAsync(model);
        }

        public List<TableDesignerColumn> GetTableColumnsAsync(int id)
        {
            return _tableDesignerRepository.GetTableColumnsAsync(id);
        }

        public bool InsertColumnsAsync(int id, List<TableDesignerColumn> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentNullException("表头不能为null");
            }

            bool result = _tableDesignerRepository.DeleteColumnsAsync(id);
            if (result)
            {
                foreach(TableDesignerColumn item in list)
                {
                    item.TableId = id;
                    _tableDesignerRepository.InsertColumnsAsync(item);
                }
            }
            return result;
        }

        public PageModel<Dictionary<string, object>> GetTableData(int pageIndex, int pageSize, int id, string dataSource, int organizationId, Dictionary<string, string> searchDic)
        {
            List<TableDesignerColumn> columns = _tableDesignerRepository.GetTableColumnsAsync(id);
            string dataSql = "select b.* from (select"; // 分页查询数据sql
            string columnSql = ""; // 查询内容sql
            string whereSql = " where s.Status = 1 and s.organizationId = " + organizationId; // 条件sql
            string leftJoin = ""; // 链接其他表sql
            string orderBy = ""; // 排序sql
            foreach (TableDesignerColumn column in columns)
            {
                string sourceName = ""; // 未加工原始字段sql
                // 拼接查询项
                if (column.TableColumn == "creatorName")
                {
                    columnSql += ",u.nickName as creatorName";
                    sourceName = "u.userName";
                    leftJoin += " left join [sys_user] u on u.userId = s.creator";
                }
                else
                {
                    sourceName = "s." + column.TableColumn;
                    // 字段加工
                    if (!column.FormulaValue.IsNullOrEmpty())
                    {
                        switch (column.Formula)
                        {
                            case 0: // 默认不加工
                                columnSql += ", s." + column.TableColumn;
                                break;
                            case 1: // 加（后续实现）
                                columnSql += ", s." + column.TableColumn;
                                break;
                            case 2: // 减（后续实现）
                                columnSql += ", s." + column.TableColumn;
                                break;
                            case 3: // 乘（后续实现）
                                columnSql += ", s." + column.TableColumn;
                                break;
                            case 4: // 除（后续实现）
                                columnSql += ", s." + column.TableColumn;
                                break;
                            case 5: // 拼接
                                columnSql += ", cast(s." + column.TableColumn + " as varchar) + '" + column.FormulaValue + "' as " + column.TableColumn;
                                break;
                            default:
                                break;
                        }
                    }
                    else 
                    {
                        columnSql += ", s." + column.TableColumn;
                    }
                }
                // 拼接条件
                if (column.SearchType == 1) // 等于
                {
                    string searchValue = "";
                    searchDic.TryGetValue(column.TableColumn, out searchValue);
                    if (!searchValue.IsNullOrEmpty()) 
                    {
                        whereSql += " and " + sourceName + " = '" + searchValue + "'";
                    }
                }
                else if (column.SearchType == 2) // 模糊查询
                {
                    string searchValue = "";
                    searchDic.TryGetValue(column.TableColumn, out searchValue);
                    if (!searchValue.IsNullOrEmpty())
                    {
                        whereSql += " and " + sourceName + " like '%" + searchValue + "%'";
                    }
                }
                else if (column.SearchType == 3) 
                {
                    string searchBeginValue = "";
                    searchDic.TryGetValue(column.TableColumn + "_begin", out searchBeginValue);
                    string searchEndValue = "";
                    searchDic.TryGetValue(column.TableColumn + "_end", out searchEndValue);
                    if (!searchBeginValue.IsNullOrEmpty())
                    {
                        whereSql += " and " + sourceName + " >= '" + searchBeginValue + "'";
                    }
                    if (!searchEndValue.IsNullOrEmpty())
                    {
                        whereSql += " and " + sourceName + " <= '" + searchEndValue + "'";
                    }
                }
                // 拼接排序项
                if (column.Orderby == 1)
                {
                    if (orderBy == "")
                    {
                        orderBy += " order by " + sourceName + " asc";
                    }
                    else
                    {
                        orderBy += " , " + sourceName + " desc";
                    }
                }
            }
            orderBy = orderBy == "" ? "order by s.id desc" : orderBy;
            dataSql += " ROW_NUMBER() over (" + orderBy + ") as RowNumber" + columnSql + " from [" + dataSource + "] s" + leftJoin + whereSql 
                + ") as b where RowNumber between " + ((pageIndex - 1) * pageSize) + 1 + " and " + pageIndex * pageSize;
            string countSql = string.Format("select count(1) from [{0}] s", dataSource); // 查询数量sql
            countSql += leftJoin;
            countSql += whereSql;
            return _tableDesignerRepository.GetTableData(countSql, dataSql);
        }
    }
}
