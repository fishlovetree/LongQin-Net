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
    public class FormDesignerService : ServiceBase, IFormDesignerService
    {
        IFormDesignerRepository _formDesignerRepository = AutofacRepository.Resolve<IFormDesignerRepository>();

        public FormDesignerService()
        {
            base.AddDisposableObject(_formDesignerRepository);
        }

        public bool CreateAsync(FormDesigner model)
        {
            model.TableName = "des_" + model.TableName + "_" + model.Creator + "_" + model.OrganizationId;
            int count = _formDesignerRepository.GetTableCountAsync(model.TableName);
            if (count > 0) return false;
            _formDesignerRepository.CreateTableAsync(model.TableName, model.TableColumns);
            return _formDesignerRepository.CreateFormAsync(model);
        }

        public PageModel<FormDesigner> GetListAsync(int organizationId, int pageIndex, int pageSize)
        {
            return _formDesignerRepository.GetListAsync(organizationId, pageIndex, pageSize);
        }

        public FormDesigner GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _formDesignerRepository.GetByIdAsync(id);
        }

        public bool DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id错误");
            }

            return _formDesignerRepository.DeleteAsync(id);
        }

        public bool UpdateAsync(FormDesigner model)
        {
            int count = _formDesignerRepository.GetTableCountAsync(model.TableName);
            if (count > 0)
            {
                _formDesignerRepository.DeleteTableAsync(model.TableName);
                _formDesignerRepository.CreateTableAsync(model.TableName, model.TableColumns);
            }
            else
            {
                _formDesignerRepository.CreateTableAsync(model.TableName, model.TableColumns);
            }
            return _formDesignerRepository.UpdateAsync(model);
        }

        public bool InertFormDataAsync(string tableName, List<string> columns, List<string> values)
        {
            return _formDesignerRepository.InsertFormDataAsync(tableName, columns, values);
        }

        // 根据数据库表名获取字段集合
        public List<TableColumn> GetTableColumnsAsync(string tableName)
        {
            var columns = _formDesignerRepository.GetTableColumnsAsync(tableName);
            if (columns != null && columns.Count != 0) 
            {
                foreach (var column in columns) 
                {
                    switch (column.ColumnName) 
                    {
                        case "id":
                            column.Description = "主键";
                            break;
                        case "creator":
                            column.Description = "创建者ID";
                            break;
                        case "createTime":
                            column.Description = "创建时间";
                            break;
                        case "status":
                            column.Description = "状态";
                            break;
                        default:
                            break;
                    }
                }
                TableColumn creatorName = new TableColumn();
                creatorName.ColumnName = "creatorName";
                creatorName.Description = "创建者名称";
                columns.Add(creatorName);
            }
            return columns;
        }
    }
}
