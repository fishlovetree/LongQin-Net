
(function ($) {
    var Flow = {
        createNew: function (pager, form, treeSelect) {
            var flow = {};
            flow.rectarr = {}; //节点集合
            flow.patharr = {}; //连线集合
            flow.begin; //连线起点(添加连线时用到)
            flow.tmp;  //临时点(为确定连线起点和终点)
            flow.end;  //连线终点(添加连线时用到)
            flow.forms;
            flow.positions;
            flow.users;
            flow.config = {
                editable: true,
                lineHeight: 15,
                basePath: "",
                rect: {
                    attr: {
                        x: 300,
                        y: 100,
                        width: 100,
                        height: 50,
                        r: 5,
                        fill: "90-#fff-#C0C0C0",
                        stroke: "#000",
                        "stroke-width": 3
                    },
                    data: { //节点属性数据
                        id: 0,
                        name: '新建节点',
                        rectType: 0,
                        formId: '',
                        cooperation: 0,
                        virtual: 0,
                        beentrusted: 1,
                        departmentId: 0,
                        positionId: 0,
                        userId: 0,
                        userName: '',
                        remark: '',
                        isApproval: 0, // 是否审批节点，1-是，0-否
                        dealed: false // 已处理过
                    },
                    text: {
                        text: "新建节点",
                        "font-size": 12
                    },
                    margin: 5
                },
                path: {
                    attr: {
                        path: {
                            path: "M10 10L100 100",
                            stroke: "#808080",
                            fill: "none",
                            "stroke-width": 4
                        },
                        arrow: {
                            path: "M10 10L10 10",
                            stroke: "#808080",
                            fill: "#808080",
                            "stroke-width": 4,
                            radius: 4
                        },
                        fromDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        toDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        bigDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 2
                        },
                        smallDot: {
                            width: 5,
                            height: 5,
                            stroke: "#fff",
                            fill: "#000",
                            cursor: "move",
                            "stroke-width": 3
                        }
                    },
                    data: { //连线属性数据
                        id: 0,
                        name: '',
                        formId: '0',
                        field: '', //条件字段
                        operator: '', //条件符号，如‘=’
                        operatorValue: '', //条件值
                        remark: '',
                        dealed: false // 已处理过
                    },
                    text: {
                        text: "",
                        cursor: "move",
                        background: "#000",
                        "font-size": 12
                    },
                    textPos: {
                        x: 0,
                        y: -10
                    }
                },
                restore: ""
            };
            flow.util = { //方法集
                isLine: function (g, f, e) {
                    var d, c;
                    if ((g.x - e.x) == 0) {
                        d = 1;
                    } else {
                        d = (g.y - e.y) / (g.x - e.x);
                    }
                    c = (f.x - e.x) * d + e.y;
                    if ((f.y - c) < 10 && (f.y - c) > -10) {
                        f.y = c;
                        return true;
                    }
                    return false;
                },
                center: function (d, c) {
                    return {
                        x: (d.x - c.x) / 2 + c.x,
                        y: (d.y - c.y) / 2 + c.y
                    }
                },
                nextId: (function () {
                    var c = 0;
                    return function () {
                        return ++c;
                    }
                })(),
                nextPathId: (function () {
                    var c = 0;
                    return function () {
                        return ++c;
                    }
                })(),
                connPoint: function (j, d) {
                    var c = d,
                    e = {
                        x: j.x + j.width / 2,
                        y: j.y + j.height / 2
                    };
                    var l = (e.y - c.y) / (e.x - c.x);
                    l = isNaN(l) ? 0 : l;
                    var k = j.height / j.width;
                    var h = c.y < e.y ? -1 : 1,
                    f = c.x < e.x ? -1 : 1,
                    g,
                    i;
                    if (Math.abs(l) > k && h == -1) {
                        g = e.y - j.height / 2;
                        i = e.x + h * j.height / 2 / l;
                    } else {
                        if (Math.abs(l) > k && h == 1) {
                            g = e.y + j.height / 2;
                            i = e.x + h * j.height / 2 / l;
                        } else {
                            if (Math.abs(l) < k && f == -1) {
                                g = e.y + f * j.width / 2 * l;
                                i = e.x - j.width / 2;
                            } else {
                                if (Math.abs(l) < k && f == 1) {
                                    g = e.y + j.width / 2 * l;
                                    i = e.x + j.width / 2;
                                }
                            }
                        }
                    }
                    return {
                        x: i,
                        y: g
                    }
                },
                arrow: function (l, k, d) {
                    var g = Math.atan2(l.y - k.y, k.x - l.x) * (180 / Math.PI);
                    var h = k.x - d * Math.cos(g * (Math.PI / 180));
                    var f = k.y + d * Math.sin(g * (Math.PI / 180));
                    var e = h + d * Math.cos((g + 120) * (Math.PI / 180));
                    var j = f - d * Math.sin((g + 120) * (Math.PI / 180));
                    var c = h + d * Math.cos((g + 240) * (Math.PI / 180));
                    var i = f - d * Math.sin((g + 240) * (Math.PI / 180));
                    return [k, {
                        x: e,
                        y: j
                    },
                    {
                        x: c,
                        y: i
                    }]
                },
                attr: function (ele, d) { //远程节点数据赋值到rect对象
                    if (ele && d) {
                        for (var p in d) {
                            ele[p] = d[p];
                        }
                    }
                },
                addpath: function (c) { //添加连线
                    if (flow.begin && flow.end) {
                        if (!flow.util.checkpath(flow.begin, flow.end)) {
                            var p = new flow.path(flow.begin, flow.end, c);
                            flow.patharr[p.getId()] = p;

                            //加载元素属性
                            //flow.util.getPathPropertie(p);

                            p.select();
                        }
                    }
                },
                getPathPropertie: function (p) {
                    $('#ElementPropertie').html('');
                    $('body').off('keyup', '#ElementPropertie #pathname');
                    var proHtml = '<div class="layui-form-item">';
                    proHtml += '<input type="hidden" value="' + p.getId() + '" id="' + 'pathid' + '">';
                    proHtml += '<label class="layui-form-label"><font color=\"red\">* </font>连线名称</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<input class="layui-input layui-keyup" value="' + p.attr({ path: 'name' }) + '" id="' + 'pathname' + '">';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">条件符号</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" data-field="operator">';
                    var operatorType = [
                        {
                            title: '请选择',
                            value: '',
                        },
                        {
                            title: '=',
                            value: '=',
                        },
                        {
                            title: '!=',
                            value: '!=',
                        },
                        {
                            title: '>',
                            value: '>',
                        },
                        {
                            title: '<',
                            value: '<',
                        },
                        {
                            title: '>=',
                            value: '>=',
                        },
                        {
                            title: '<=',
                            value: '<=',
                        }
                    ];
                    for (let index = 0; index < operatorType.length; index++) {
                        const element = operatorType[index];
                        proHtml += '<option value="' + element.value + '"';
                        if (element.value == p.attr({ path: 'operator' })) {
                            proHtml += ' selected';
                        }
                        proHtml += '>' + element.title + '</option>';
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">条件表单</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" name="formId" lay-search data-field="formId" id="formId"><option value="">请选择</option>';
                    if (flow.forms) {
                        for (let index = 0; index < flow.forms.length; index++) {
                            const element = flow.forms[index];
                            proHtml += '<option value="' + element.Id + '"';
                            if (element.Id == p.attr({ path: 'formId' })) {
                                proHtml += ' selected';
                            }
                            proHtml += '>' + element.FormName + '</option>';
                        }
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">条件字段</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" lay-search data-field="field" id="field"><option value="">请选择</option>';
                    var data = [];
                    data.push({ 'label': '提交人', 'name': 'userId' });
                    data.push({ 'label': '提交人职级', 'name': 'positionLevel' });
                    var fromFormId = p.attr({ path: 'formId' }) != '0' ? p.attr({ path: 'formId' }) : p.from().attr('formId');
                    if (fromFormId) {
                        $.ajax({
                            url: "/FormDesigner/FormDesign/GetById",    //后台数据请求地址
                            type: "get",
                            data: { id: fromFormId },
                            async: false,
                            success: function (slt) {
                                if (slt) {
                                    var formData = JSON.parse(slt).data;
                                    var jsonData = JSON.parse(formData.JsonData);
                                    data = data.concat(jsonData);
                                    for (let index = 0; index < data.length; index++) {
                                        const element = data[index];
                                        proHtml += '<option value="' + element.name + '"';
                                        if (element.name == p.attr({ path: 'field' })) {
                                            proHtml += ' selected';
                                        }
                                        proHtml += '>' + element.label + '</option>';
                                    }
                                }
                            }
                        });
                    }
                    else {
                        for (let index = 0; index < data.length; index++) {
                            const element = data[index];
                            proHtml += '<option value="' + element.name + '"';
                            if (element.name == p.attr({ path: 'field' })) {
                                proHtml += ' selected';
                            }
                            proHtml += '>' + element.label + '</option>';
                        }
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">条件值</label>';
                    proHtml += '<div class="layui-input-inline" id="operatorValueDiv">';
                    var selectFiled = p.attr({ path: 'field' });
                    if (selectFiled == 'positionLevel') {
                        proHtml += '<select lay-filter="componentSelected" lay-search data-field="operatorValue">';
                        if (p.attr({ path: 'operatorValue' }) == 1) {
                            proHtml += '<option value="1" selected>基层</option><option value="2">中层</option><option value="3">高层</option>';
                        }
                        else if (p.attr({ path: 'operatorValue' }) == 2) {
                            proHtml += '<option value="1">基层</option><option value="2" selected>中层</option><option value="3">高层</option>';
                        }
                        else if (p.attr({ path: 'operatorValue' }) == 2) {
                            proHtml += '<option value="1">基层</option><option value="2">中层</option><option value="3" selected>高层</option>';
                        }
                        else {
                            proHtml += '<option value="1">基层</option><option value="2">中层</option><option value="3">高层</option>';
                        }
                        proHtml += '</select>';
                    }
                    else if (selectFiled == 'userId') {
                        proHtml += '<select lay-filter="componentSelected" lay-search data-field="operatorValue">';
                        if (flow.users) {
                            for (let index = 0; index < flow.users.length; index++) {
                                const element = flow.users[index];
                                proHtml += '<option value="' + element.UserId + '"';
                                if (element.UserId == p.attr({ path: 'operatorValue' })) {
                                    proHtml += ' selected';
                                }
                                proHtml += '>' + element.UserName + '</option>';
                            }
                        }
                        proHtml += '</select>';
                    }
                    else {
                        proHtml += '<input class="layui-input layui-keyup" value="' + p.attr({ path: 'operatorValue' }) + '" id="' + 'operatorValue' + '">';
                    }
                    proHtml += '</div></div>';

                    proHtml += '<div id="slideTest8" ></div>';
                    $('#ElementPropertie').html(proHtml);
                    form.render(null, 'ElementPropertie');

                    $('body').on('keyup', '#ElementPropertie #pathname', function (e) {
                        var id = $('#ElementPropertie #pathid').val();
                        //flow.patharr[id]['name'] = $(this).val();
                        flow.patharr[id].setattr('name', $(this).val());
                        flow.patharr[id].settext($(this).val());
                    })
                    $('body').on('keyup', '#ElementPropertie #operatorValue', function (e) {
                        var id = $('#ElementPropertie #pathid').val();
                        flow.patharr[id].setattr('operatorValue', $(this).val());
                    })
                    form.on('select(componentSelected)', function (data) {
                        var field = $(data.elem).data('field'), id = $('#ElementPropertie #pathid').val()
                            , element = flow.patharr[id];
                        element[field] = data.value;
                        element.setattr(field, data.value);

                        // 加载条件值选项
                        if (field == 'field') {
                            if (data.value == 'positionLevel') {
                                var subHtml = '<select lay-filter="componentSelected" lay-search data-field="operatorValue">';
                                if (p.attr({ path: 'operatorValue' }) == 1) {
                                    subHtml += '<option value="1" selected>基层</option><option value="2">中层</option><option value="3">高层</option>';
                                }
                                else if (p.attr({ path: 'operatorValue' }) == 2) {
                                    subHtml += '<option value="1">基层</option><option value="2" selected>中层</option><option value="3">高层</option>';
                                }
                                else if (p.attr({ path: 'operatorValue' }) == 2) {
                                    subHtml += '<option value="1">基层</option><option value="2">中层</option><option value="3" selected>高层</option>';
                                }
                                else {
                                    subHtml += '<option value="1">基层</option><option value="2">中层</option><option value="3">高层</option>';
                                }
                                subHtml += '</select>';
                                $('#operatorValueDiv').html(subHtml);
                            }
                            else if (data.value == 'userId') {
                                var subHtml = '<select lay-filter="componentSelected" lay-search data-field="operatorValue">';
                                if (flow.users) {
                                    for (let index = 0; index < flow.users.length; index++) {
                                        const element = flow.users[index];
                                        subHtml += '<option value="' + element.UserId + '"';
                                        if (element.UserId == p.attr({ path: 'operatorValue' })) {
                                            subHtml += ' selected';
                                        }
                                        subHtml += '>' + element.UserName + '</option>';
                                    }
                                }
                                subHtml += '</select>';
                                $('#operatorValueDiv').html(subHtml);
                            }
                            else {
                                var subHtml = '<input class="layui-input layui-keyup" value="' + p.attr({ path: 'operatorValue' }) + '" id="' + 'operatorValue' + '">';
                                $('#operatorValueDiv').html(subHtml);
                            }
                            form.render(null, 'ElementPropertie');
                        }
                        else if (field == 'formId') {
                            var subdata = [];
                            $('#field').html('');
                            var subHtml = '<option value="">请选择</option>';
                            subdata.push({ 'label': '提交人', 'name': 'userId' });
                            subdata.push({ 'label': '提交人职级', 'name': 'positionLevel' });
                            var fromFormId = data.value ? data.value : p.from().attr('formId');
                            if (fromFormId) {
                                $.ajax({
                                    url: "/FormDesigner/FormDesign/GetById",    //后台数据请求地址
                                    type: "get",
                                    data: { id: fromFormId },
                                    async: false,
                                    success: function (slt) {
                                        if (slt) {
                                            var formData = JSON.parse(slt).data;
                                            var jsonData = JSON.parse(formData.JsonData);
                                            subdata = subdata.concat(jsonData);
                                            for (let index = 0; index < subdata.length; index++) {
                                                const element = subdata[index];
                                                subHtml += '<option value="' + element.name + '"';
                                                if (element.name == p.attr({ path: 'field' })) {
                                                    subHtml += ' selected';
                                                }
                                                subHtml += '>' + element.label + '</option>';
                                            }
                                            $('#field').append($(subHtml));
                                        }
                                    }
                                });
                            }
                            else {
                                for (let index = 0; index < data.length; index++) {
                                    const element = data[index];
                                    subHtml += '<option value="' + element.name + '"';
                                    if (element.name == p.attr({ path: 'field' })) {
                                        subHtml += ' selected';
                                    }
                                    subHtml += '>' + element.label + '</option>';
                                }
                                $('#field').append($(subHtml));
                            }
                            form.render('select');
                        }
                    })
                },
                checkpath: function (begin, end) { //检查连线是否存在
                    for (var p in flow.patharr) {
                        if (flow.patharr[p]) {
                            if ((flow.patharr[p].from().getId() == begin.getId() && flow.patharr[p].to().getId() == end.getId())
                                || (flow.patharr[p].from().getId() == end.getId() && flow.patharr[p].to().getId() == begin.getId())) {
                                return true;
                            }
                        }
                    }
                    return false;
                },
                addrect: function () { //添加节点
                    var p = new flow.rect();
                    flow.rectarr[p.getId()] = p;

                    //加载元素属性
                    //flow.util.getRectPropertie(p);

                    p.select();
                },
                getRectPropertie: function (p) {
                    $('#ElementPropertie').html('');
                    $('body').off('click', '.layui-btn-designer');
                    $('body').off('keyup', '#ElementPropertie #name');
                    var proHtml = '<div class="layui-form-item">';
                    proHtml += '<input type="hidden" value="' + p.getId() + '" id="' + 'pid' + '">';
                    proHtml += '<label class="layui-form-label"><font color=\"red\">* </font>节点名称</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<input class="layui-input layui-keyup" value="' + p.attr('name') + '" id="' + 'name' + '">';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label"><font color=\"red\">* </font>节点类型</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" data-field="rectType">';
                    var nodeType = [
                        {
                            title: '普通节点',
                            value: '0',
                        },
                        {
                            title: '分流节点',
                            value: '1',
                        },
                        {
                            title: '合流节点',
                            value: '2',
                        },
                        {
                            title: '分合流点',
                            value: '3',
                        }
                    ];
                    for (let index = 0; index < nodeType.length; index++) {
                        const element = nodeType[index];
                        proHtml += '<option value="' + element.value + '"';
                        if (element.value == p.attr('rectType')) {
                            proHtml += ' selected';
                        }
                        proHtml += '>' + element.title + '</option>';
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">是否审批</label>';
                    proHtml += '<div class="layui-input-inline">';
                    if (p.attr('isApproval') == 0) {
                        proHtml += '<input type="radio" name="isApproval" value="1" lay-filter="isApproval" title="是">';
                        proHtml += '<input type="radio" name="isApproval" value="0" lay-filter="isApproval" title="否" checked>';
                    }
                    else {
                        proHtml += '<input type="radio" name="isApproval" value="1" lay-filter="isApproval" title="是" checked>';
                        proHtml += '<input type="radio" name="isApproval" value="0" lay-filter="isApproval" title="否">';
                    }
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item" id="nodeFormDiv">';
                    proHtml += '<label class="layui-form-label">节点表单</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" name="formId" lay-search data-field="formId"><option value="">请选择</option>';
                    if (flow.forms) {
                        for (let index = 0; index < flow.forms.length; index++) {
                            const element = flow.forms[index];
                            proHtml += '<option value="' + element.Id + '"';
                            if (element.Id == p.attr('formId')) {
                                proHtml += ' selected';
                            }
                            proHtml += '>' + element.FormName + '</option>';
                        }
                    }
                    proHtml += '</select>';
                    proHtml += '<button type="button" class="layui-btn layui-btn-sm layui-btn-designer">设计</button>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">多人协作</label>';
                    proHtml += '<div class="layui-input-inline">';
                    if (p.attr('cooperation') == 0) {
                        proHtml += '<input type="radio" name="cooperation" value="1" lay-filter="cooperation" title="是">';
                        proHtml += '<input type="radio" name="cooperation" value="0" lay-filter="cooperation" title="否" checked>';
                    }
                    else {
                        proHtml += '<input type="radio" name="cooperation" value="1" lay-filter="cooperation" title="是" checked>';
                        proHtml += '<input type="radio" name="cooperation" value="0" lay-filter="cooperation" title="否">';
                    }
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">处理部门</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<input class="layui-input" lay-filter="departmentId" value="' + p.attr('departmentId') + '" id="' + 'departmentId' + '">';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">处理职位</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" lay-search data-field="positionId"><option value="0">请选择</option>';
                    if (flow.positions) {
                        for (let index = 0; index < flow.positions.length; index++) {
                            const element = flow.positions[index];
                            proHtml += '<option value="' + element.PositionId + '"';
                            if (element.PositionId == p.attr('positionId')) {
                                proHtml += ' selected';
                            }
                            proHtml += '>' + element.PositionName + '</option>';
                        }
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div class="layui-form-item">';
                    proHtml += '<label class="layui-form-label">处理人</label>';
                    proHtml += '<div class="layui-input-inline">';
                    proHtml += '<select lay-filter="componentSelected" lay-search data-field="userId"><option value="0">请选择</option>';
                    if (flow.users) {
                        for (let index = 0; index < flow.users.length; index++) {
                            const element = flow.users[index];
                            proHtml += '<option value="' + element.UserId + '"';
                            if (element.UserId == p.attr('userId')) {
                                proHtml += ' selected';
                            }
                            proHtml += '>' + element.UserName + '</option>';
                        }
                    }
                    proHtml += '</select>';
                    proHtml += '</div></div>';

                    proHtml += '<div id="slideTest8" ></div>';
                    $('#ElementPropertie').html(proHtml);
                    form.render(null, 'ElementPropertie');

                    $('body').on('click', '.layui-btn-designer', function (e) {
                        layer.open({
                            type: 2
                            , content: '/FlowDesigner/FlowDesign/FormDesigner'
                            , area: 'auto'
                            , shade: false
                            , resize: true
                            , maxmin: false
                            , success: function (layero, index) {
                                layer.full(index);
                                var iframeWin = window[layero.find('iframe')[0]['name']];
                                var elemMark = iframeWin.$('#layerIndex'); // 获得 iframe 中某个输入框元素
                                elemMark.val(index);
                            }
                        });
                    })

                    treeSelect.render({
                        // 选择器
                        elem: '#departmentId',
                        // 异步获取下拉树需要显示的数据
                        data: '/System/Department/GetDepartmentTree',
                        // 异步加载方式：get/post，默认get
                        type: 'get',
                        // 占位符
                        placeholder: '处理部门',
                        // 是否开启搜索功能：true/false，默认false
                        search: true,
                        // 一些可定制的样式
                        style: {
                            folder: {
                                enable: true
                            },
                            line: {
                                enable: true
                            }
                        },
                        // 点击节点回调
                        click: function (d) {
                            var id = $('#ElementPropertie #pid').val()
                                , element = flow.rectarr[id];
                            element.setattr('departmentId', d.current.id);
                        },
                        // 加载完成后的回调函数
                        success: function (d) {
                            //console.log(d);
                            // 选中节点，根据id筛选，一般修改时会有默认选中状态，可以在这里设置
                            if ($("#departmentId").val() != '0') {
                                treeSelect.checkNode('departmentId', $("#departmentId").val());
                            }
                        }
                    });

                    $('body').on('keyup', '#ElementPropertie #name', function (e) {
                        var id = $('#ElementPropertie #pid').val();
                        //flow.rectarr[id]['name'] = $(this).val();
                        flow.rectarr[id].setattr('name', $(this).val());
                        flow.rectarr[id].settext($(this).val());
                    })
                    form.on('radio(cooperation)', function (data) {
                        var elem = data.elem; // 获得 radio 原始 DOM 对象
                        var value = elem.value; // 获得 radio 值

                        var id = $('#ElementPropertie #pid').val();
                        flow.rectarr[id].setattr('cooperation', value);
                    });
                    form.on('radio(isApproval)', function (data) {
                        var elem = data.elem; // 获得 radio 原始 DOM 对象
                        var value = elem.value; // 获得 radio 值

                        var id = $('#ElementPropertie #pid').val();
                        flow.rectarr[id].setattr('isApproval', value);
                        var element = flow.rectarr[id];
                        if (value == 1) {
                            $('#nodeFormDiv').hide();
                            element['formId'] = 1;
                            element.setattr('formId', 1);
                        }
                        else {
                            $('#nodeFormDiv').show();
                            form.val('mainForm', { 'formId': '' });
                        }
                    });
                    form.on('select(componentSelected)', function (data) {
                        var field = $(data.elem).data('field'), id = $('#ElementPropertie #pid').val()
                            , element = flow.rectarr[id];
                        element[field] = data.value;
                        element.setattr(field, data.value);
                    })
                },
                check: function () { //流程检查
                    if (flow.patharr.length == 0 || flow.rectarr.length == 0) return false;
                    return true;
                },
                save: function () { //保存
                    var flowId = $("#flowId").val();
                    var flowName = $("#flowName").val();
                    if (!flowName) {
                        layer.msg("请输入流程名");
                        return;
                    }
                    if (flow.util.check()) {
                        var nodes = "";
                        for (var rect in flow.rectarr) {
                            if (flow.rectarr[rect]) {
                                nodes += flow.rectarr[rect].toJson() + ";";
                            }
                        }
                        if (nodes.substring(nodes.length - 1, nodes.length) == ";") {
                            nodes = nodes.substring(0, nodes.length - 1);
                        }
                        var links = "";
                        for (var path in flow.patharr) {
                            if (flow.patharr[path]) {
                                links += flow.patharr[path].toJson() + ";";
                            }
                        }
                        if (links == "") {
                            layer.msg("请检查流程");
                            return;
                        }
                        if (links.substring(links.length - 1, links.length) == ";") {
                            links = links.substring(0, links.length - 1);
                        }
                        var flowSort = $("#sort").val();
                        $.ajax({
                            url: "/FlowDesigner/FlowDesign/Save",    //后台数据请求地址
                            type: "post",
                            data: { flowId: flowId, flowName: flowName, flowSort: flowSort, nodes: nodes, links: links },
                            async: false,
                            success: function (result) {
                                if (result && result.success) {
                                    layer.msg("保存成功");
                                }
                                else {
                                    layer.msg("保存失败，请重试");
                                }
                            }
                        });
                    }
                },
                groupSeq: function (r) { //得到节点的序号
                    var beginNum = 0; //起点连线数量
                    var endNum = 0; //终点连线数量
                    for (var path in flow.patharr) {
                        if (flow.patharr[path] && flow.patharr[path].from().getId() == r.getId()) {
                            beginNum++;
                        }
                        if (flow.patharr[path] && flow.patharr[path].to().getId() == r.getId()) {
                            endNum++;
                        }
                    }
                    if (beginNum > 0 && endNum == 0) { //起点
                        return 1;
                    }
                    else if (beginNum == 0 && endNum > 0) { //终点
                        return 9;
                    }
                    else {
                        return 2;
                    }
                }
            };
            flow.rect = function (rect) {
                var u = this;
                var nextId = flow.util.nextId();
                var g = "rect" + nextId;
                var a;
                if (rect) {
                    a = $.extend(true, {}, flow.config.rect, rect);
                }
                else {
                    a = flow.config.rect;
                    a.attr.y = 100 + (nextId - 1) * 120;
                }
                var t = pager.rect(a.attr.x, a.attr.y, a.attr.width, a.attr.height, a.attr.r).attr(a.attr);
                flow.util.attr(t, a.data); //节点属性
                if (a.data.dealed) t.attr("fill", "90-#fff-#0b92d5");
                var f = pager.text(a.attr.x + a.attr.width / 2, a.attr.y + a.attr.height / 2, a.text.text).attr(a.text);
                var n = pager.text(a.attr.x + 120, a.attr.y + 8, '').attr("fill", "rgb(20, 165, 236)");
                var q = {
                    x: a.attr.x - a.margin,
                    y: a.attr.y - a.margin,
                    width: a.attr.width + a.margin * 2,
                    height: a.attr.height + a.margin * 2
                };
                var x, v;
                t.drag(function (r, o) {
                    A(r, o)
                },
                function () {
                    z()
                },
                function () {
                    l()
                });
                f.drag(function (r, o) {
                    A(r, o)
                },
                function () {
                    z()
                },
                function () {
                    l()
                });
                n.drag(function (r, o) {
                    A(r, o)
                },
                function () {
                    z()
                },
                function () {
                    l()
                });
                var A = function (dx, dy) {
                    var o = (x + dx);
                    var G = (v + dy);
                    q.x = o - a.margin;
                    q.y = G - a.margin;
                    B();
                };
                var z = function () {
                    x = t.attr("x");
                    v = t.attr("y");
                    t.attr({
                        opacity: 0.5
                    });
                    f.attr({
                        opacity: 0.5
                    });
                };
                var l = function () {
                    t.attr({
                        opacity: 1
                    });
                    f.attr({
                        opacity: 1
                    });
                };

                $([t.node, f.node]).bind("click", function (e) {
                    if ($(pager).data("currNode") != u) {
                        t.attr("fill", "90-#fff-#0b92d5");
                        if (flow.begin) {
                            if (flow.begin != u) {
                                if (flow.end) {
                                    if (flow.end != u) {
                                        n.show();
                                        n.attr("text", "[后继]");
                                        flow.tmp = flow.end;
                                        flow.end = u;
                                    }
                                }
                                else {
                                    n.show();
                                    n.attr("text", "[后继]");
                                    flow.end = u;
                                }
                            }
                            else {
                                if (flow.end) {
                                    n.show();
                                    n.attr("text", "[后继]");
                                    flow.tmp = flow.end;
                                    flow.end = u;
                                }
                            }
                        }
                        else {
                            n.show();
                            n.attr("text", "[前置]");
                            flow.begin = u;
                        }
                        $(pager).trigger("click", u);
                        $(pager).data("currNode", u);
                        flow.util.getRectPropertie(flow.rectarr[g]);
                    }
                });

                var j = function (o, r) {
                    if (r.getId() != g) {
                        t.attr("fill", "90-#fff-#C0C0C0");
                        if (r.getId().substring(0, 4) == "rect") {
                            if (flow.begin == u) {
                                //终点非当前选中节点
                                if (flow.tmp && flow.tmp.getId() != r.getId()) {
                                    n.hide();
                                    n.attr("text", '');
                                }
                            }
                            else if (flow.tmp == u) { //终点改为起点
                                n.show();
                                n.attr("text", "[前置]");
                                flow.begin = u;
                            }
                            else {
                                n.hide();
                                n.attr("text", '');
                            }
                        }
                    }
                };
                $(pager).bind("click", j);

                $([t.node, f.node]).bind("dblclick", function () {
                    //alert(t['rectType'])
                });

                function B() {
                    var F = q.x + a.margin,
                    r = q.y + a.margin,
                    G = q.width - a.margin * 2,
                    o = q.height - a.margin * 2;
                    t.attr({
                        x: F,
                        y: r,
                        width: G,
                        height: o
                    });
                    f.attr({
                        x: F + G / 2,
                        y: r + o / 2
                    });
                    n.attr({
                        x: F + 120,
                        y: r + 8
                    });
                    $(pager).trigger("rectresize", u)
                }
                this.toJson = function () {
                    var seq = flow.util.groupSeq(u);
                    var r = g + "," + Math.round(t.attr("x")) + "," + Math.round(t.attr("y")) + "," + t["id"] + "," + t["name"]
                    + "," + t["rectType"] + "," + t["formId"] + "," + t["virtual"] + "," + t["cooperation"]
                    + "," + t["departmentId"] + "," + t["positionId"] + "," + t["userId"] + "," + t["remark"] + "," + seq + "," + t["isApproval"];
                    //                    var r = "{TmpID:'" + g + "',X:" + Math.round(t.attr("x")) + ",Y:" + Math.round(t.attr("y")) + ",NodeID:"
                    //                    + t["id"] + ",NodeName:'" + t["name"] + "',NodeType:" + t["recttype"] + ",MainMenu:'" + t["mainmenu"]
                    //                    + "',CopyMenu:'" + t["copymenu"] + "',Virtual:'" + t["virtual"] + "',Cooperation:'" + t["cooperation"]
                    //                    + "',Dept:'" + t["dept"] + "',Role:'" + t["role"] + "',Post:'" + t["position"] + "',User:'" + t["userid"]
                    //                    + "',Description:'" + t["remark"] + "'";
                    //                    r += "}";
                    return r;
                };
                this.getBBox = function () {
                    return q;
                };
                this.getId = function () {
                    return g;
                };
                this.text = function () {
                    return f.attr("text");
                };
                this.settext = function (text) {
                    f.attr("text", text);
                };
                this.attr = function (o) {
                    if (o) {
                        return t[o];
                    }
                };
                this.setattr = function (o, v) {
                    if (o) {
                        t[o] = v;
                    }
                };
                this.remove = function () {
                    t.remove();
                    f.remove();
                    n.remove();
                };
                this.select = function () {
                    $([t.node, f.node]).trigger('click');
                };
            };
            flow.path = function (begin, end, path) {
                var v = this;
                var i,
                t,
                f,
                y,
                w,
                x;
                var a;
                if (path) {
                    a = $.extend(true, {}, flow.config.path, path);
                }
                else {
                    a = flow.config.path;
                }
                var h = a.textPos;
                var g = "path" + flow.util.nextPathId();
                //绘制连线上的点
                function p(G, H, D, L) {
                    var F = this,
                    M = G,
                    r, o = D,
                    O = L,
                    K, I, N = H;
                    switch (M) {
                        case "from":
                            r = pager.rect(H.x - a.attr.fromDot.width / 2, H.y - a.attr.fromDot.height / 2, a.attr.fromDot.width, a.attr.fromDot.height).attr(a.attr.fromDot);
                            break;
                        case "big":
                            r = pager.rect(H.x - a.attr.bigDot.width / 2, H.y - a.attr.bigDot.height / 2, a.attr.bigDot.width, a.attr.bigDot.height).attr(a.attr.bigDot);
                            break;
                        case "small":
                            r = pager.rect(H.x - a.attr.smallDot.width / 2, H.y - a.attr.smallDot.height / 2, a.attr.smallDot.width, a.attr.smallDot.height).attr(a.attr.smallDot);
                            break;
                        case "to":
                            r = pager.rect(H.x - a.attr.toDot.width / 2, H.y - a.attr.toDot.height / 2, a.attr.toDot.width, a.attr.toDot.height).attr(a.attr.toDot);
                            break
                    }
                    if (r && (M == "big" || M == "small")) {
                        r.drag(function (Q, P) { //拖动处理函数
                            C(Q, P)
                        },
                        function () { //拖动开始的处理函数
                            J()
                        },
                        function () { //拖动结束的处理函数
                            E()
                        });
                        var C = function (R, Q) {
                            var P = (K + R), S = (I + Q);
                            F.moveTo(P, S)
                        };
                        var J = function () {
                            if (M == "big") {
                                K = r.attr("x") + a.attr.bigDot.width / 2;
                                I = r.attr("y") + a.attr.bigDot.height / 2
                            }
                            if (M == "small") {
                                K = r.attr("x") + a.attr.smallDot.width / 2;
                                I = r.attr("y") + a.attr.smallDot.height / 2
                            }
                        };
                        var E = function () { }
                    }
                    this.type = function (P) {
                        if (P) {
                            M = P
                        } else {
                            return M
                        }
                    };
                    this.node = function (P) {
                        if (P) {
                            r = P
                        } else {
                            return r
                        }
                    };
                    this.left = function (P) {
                        if (P) {
                            o = P
                        } else {
                            return o
                        }
                    };
                    this.right = function (P) {
                        if (P) {
                            O = P
                        } else {
                            return O
                        }
                    };
                    this.remove = function () {
                        o = null;
                        O = null;
                        r.remove()
                    };
                    this.pos = function (P) {
                        if (P) {
                            N = P;
                            r.attr({
                                x: N.x - r.attr("width") / 2,
                                y: N.y - r.attr("height") / 2
                            });
                            return this
                        } else {
                            return N
                        }
                    };
                    this.moveTo = function (Q, T) {
                        this.pos({
                            x: Q,
                            y: T
                        });
                        switch (M) {
                            case "from":
                                if (O && O.right() && O.right().type() == "to") {
                                    O.right().pos(flow.util.connPoint(end.getBBox(), N))
                                }
                                if (O && O.right()) {
                                    O.pos(flow.util.center(N, O.right().pos()))
                                }
                                break;
                            case "big":
                                if (O && O.right() && O.right().type() == "to") {
                                    O.right().pos(flow.util.connPoint(end.getBBox(), N))
                                }
                                if (o && o.left() && o.left().type() == "from") {
                                    o.left().pos(flow.util.connPoint(begin.getBBox(), N))
                                }
                                if (O && O.right()) {
                                    O.pos(flow.util.center(N, O.right().pos()))
                                }
                                if (o && o.left()) {
                                    o.pos(flow.util.center(N, o.left().pos()))
                                }
                                var S = {
                                    x: N.x,
                                    y: N.y
                                };
                                if (flow.util.isLine(o.left().pos(), S, O.right().pos())) {
                                    M = "small";
                                    r.attr(a.attr.smallDot);
                                    this.pos(S);
                                    var P = o;
                                    o.left().right(o.right());
                                    o = o.left();
                                    P.remove();
                                    var R = O;
                                    O.right().left(O.left());
                                    O = O.right();
                                    R.remove()
                                }
                                break;
                            case "small":
                                if (o && O && !flow.util.isLine(o.pos(), {
                                    x: N.x,
                                    y: N.y
                                }, O.pos())) {
                                    M = "big";
                                    r.attr(a.attr.bigDot);
                                    var P = new p("small", flow.util.center(o.pos(), N), o, o.right());
                                    o.right(P);
                                    o = P;
                                    var R = new p("small", flow.util.center(O.pos(), N), O.left(), O);
                                    O.left(R);
                                    O = R
                                }
                                break;
                            case "to":
                                if (o && o.left() && o.left().type() == "from") {
                                    o.left().pos(flow.util.connPoint(begin.getBBox(), N))
                                }
                                if (o && o.left()) {
                                    o.pos(flow.util.center(N, o.left().pos()))
                                }
                                break
                        }
                        m()
                    }
                }
                function j() {
                    var D, C, E = begin.getBBox(), //起点属性
                    F = end.getBBox(), //终点属性
                    r,
                    o;
                    r = flow.util.connPoint(E, {
                        x: F.x + F.width / 2,
                        y: F.y + F.height / 2
                    });
                    o = flow.util.connPoint(F, r);
                    D = new p("from", r, null, new p("small", {
                        x: (r.x + o.x) / 2,
                        y: (r.y + o.y) / 2
                    }));
                    D.right().left(D);
                    C = new p("to", o, D.right(), null);
                    D.right().right(C);
                    this.toPathString = function () {
                        if (!D) {
                            return ""
                        }
                        var J = D,
                        I = "M" + J.pos().x + " " + J.pos().y,
                        H = "";
                        while (J.right()) {
                            J = J.right();
                            I += "L" + J.pos().x + " " + J.pos().y
                        }
                        var G = flow.util.arrow(J.left().pos(), J.pos(), a.attr.arrow.radius);
                        H = "M" + G[0].x + " " + G[0].y + "L" + G[1].x + " " + G[1].y + "L" + G[2].x + " " + G[2].y + "z";
                        return [I, H]
                    };
                    this.toJson = function () {
                        var G = "[", H = D;
                        while (H) {
                            if (H.type() == "big") {
                                G += "{x:" + Math.round(H.pos().x) + ",y:" + Math.round(H.pos().y) + "},"
                            }
                            H = H.right()
                        }
                        if (G.substring(G.length - 1, G.length) == ",") {
                            G = G.substring(0, G.length - 1)
                        }
                        G += "]";
                        return G
                    };
                    this.restore = function (H) {
                        var I = H, J = D.right();
                        for (var G = 0; G < I.length; G++) {
                            J.moveTo(I[G].x, I[G].y);
                            J.moveTo(I[G].x, I[G].y);
                            J = J.right()
                        }
                        this.hide()
                    };
                    this.fromDot = function () {
                        return D
                    };
                    this.toDot = function () {
                        return C
                    };
                    this.midDot = function () {
                        var H = D.right(), G = D.right().right();
                        while (G.right() && G.right().right()) {
                            G = G.right().right();
                            H = H.right()
                        }
                        return H
                    };
                    this.show = function () {
                        var G = D;
                        while (G) {
                            G.node().show();
                            G = G.right()
                        }
                    };
                    this.hide = function () {
                        var G = D;
                        while (G) {
                            G.node().hide();
                            G = G.right()
                        }
                    };
                    this.remove = function () {
                        var G = D;
                        while (G) {
                            if (G.right()) {
                                G = G.right();
                                G.left().remove()
                            } else {
                                G.remove();
                                G = null
                            }
                        }
                    }
                }
                i = pager.path(a.attr.path.path).attr(a.attr.path);
                flow.util.attr(i, a.data);
                t = pager.path(a.attr.arrow.path).attr(a.attr.arrow);
                if (a.data.dealed) {
                    i.attr("stroke", "#0b92d5");
                    t.attr("stroke", "#0b92d5");
                }
                x = new j();
                x.hide();
                f = pager.text(0, 0, a.text.text).attr(a.text);
                f.drag(function (r, o) {
                    if (!flow.config.editable) {
                        return
                    }
                    f.attr({
                        x: y + r,
                        y: w + o
                    })
                },
                function () {
                    y = f.attr("x");
                    w = f.attr("y")
                },
                function () {
                    var o = x.midDot().pos();
                    h = {
                        x: f.attr("x") - o.x,
                        y: f.attr("y") - o.y
                    }
                });
                m();
                //连线点击事件
                $([i.node, t.node, f.node]).bind("click", function () {
                    $(pager).trigger("click", v);
                    $(pager).data("currNode", v);

                    flow.util.getPathPropertie(flow.patharr[g]);
                    return false
                });
                //pager点击事件
                var l = function (r, C) {
                    if (C && C.getId() == g) {
                        x.show();
                    } else {
                        x.hide()
                    }
                };
                $(pager).bind("click", l);
                //双击事件
                $([i.node, t.node, f.node]).bind("dblclick", function () {
                    //flow.util.showPathAttr(i, f);
                });
                //删除节点事件（每条连线都会触发）
                var A = function (o, r) {
                    if (!flow.config.editable) {
                        return
                    }
                    if (r && (r.getId() == begin.getId() || r.getId() == end.getId())) {
                        $(pager).trigger("removepath", v)
                    }
                };
                $(pager).bind("removerect", A);
                var d = function (C, D) {
                    if (begin && begin.getId() == D.getId()) {
                        var o;
                        if (x.fromDot().right().right().type() == "to") {
                            o = {
                                x: end.getBBox().x + end.getBBox().width / 2,
                                y: end.getBBox().y + end.getBBox().height / 2
                            }
                        } else {
                            o = x.fromDot().right().right().pos()
                        }
                        var r = flow.util.connPoint(begin.getBBox(), o);
                        x.fromDot().moveTo(r.x, r.y);
                        m();
                    }
                    if (end && end.getId() == D.getId()) {
                        var o;
                        if (x.toDot().left().left().type() == "from") {
                            o = {
                                x: begin.getBBox().x + begin.getBBox().width / 2,
                                y: begin.getBBox().y + begin.getBBox().height / 2
                            }
                        } else {
                            o = x.toDot().left().left().pos()
                        }
                        var r = flow.util.connPoint(end.getBBox(), o);
                        x.toDot().moveTo(r.x, r.y);
                        m();
                    }
                };
                $(pager).bind("rectresize", d);
                this.from = function () {
                    return begin
                };
                this.to = function () {
                    return end
                };
                this.toJson = function () {
                    //                    var r = "{From:'" + begin.getId() + "',To:'" + end.getId() + "',LinkName:'" + f.attr("text") + "',X:" + Math.round(h.x) + ",Y:" + Math.round(h.y) + ",Operator:'"
                    //                    + i["operatortext"] + "',OperatorValue:'" + i["condition"] + "',Description:'" + i["remark"] + "'";
                    //                    r += "}";
                    var r = begin.getId() + "," + end.getId() + "," + i["name"] + "," + Math.round(h.x) + "," + Math.round(h.y) + ","
                        + i["formId"] + "," + i["field"] + "," + i["operator"] + "," + i["operatorValue"] + "," + i["remark"];
                    return r;
                };
                this.restore = function (o) {
                    var r = o;
                    a = $.extend(true, a, o);
                    x.restore(r.dots)
                };
                this.remove = function () {
                    x.remove();
                    i.remove();
                    t.remove();
                    f.remove();
                    try {
                        $(pager).unbind("click", l)
                    } catch (o) { }
                    try {
                        $(pager).unbind("removerect", A)
                    } catch (o) { }
                    try {
                        $(pager).unbind("rectresize", d)
                    } catch (o) { }
                };
                function m() {
                    var r = x.toPathString(), o = x.midDot().pos();
                    i.attr({
                        path: r[0]
                    });
                    t.attr({
                        path: r[1]
                    });
                    f.attr({
                        x: o.x + h.x,
                        y: o.y + h.y
                    })
                }
                this.getId = function () {
                    return g
                };
                this.text = function () {
                    return f.attr("text")
                };
                this.attr = function (o) {
                    if (o && o.path) {
                        return i[o.path]
                    }
                    if (o && o.arrow) {
                        return t[o.arrow]
                    }
                }
                this.settext = function (text) {
                    f.attr("text", text);
                };
                this.setattr = function (o, v) {
                    if (o) {
                        i[o] = v;
                    }
                };
                this.select = function () {
                    $([i.node, t.node, f.node]).trigger('click');
                };
            };
            flow.init = function (d, steps) {
                //Delete按键删除事件
                $(document).keydown(function (i) {
                    if (i.keyCode == 46) {
                        var j = $(pager).data("currNode");
                        if (j) {
                            if (j.getId().substring(0, 4) == "rect") {
                                $(pager).trigger("removerect", j)
                            } else {
                                if (j.getId().substring(0, 4) == "path") {
                                    $(pager).trigger("removepath", j)
                                }
                            }
                            $(pager).removeData("currNode");
                        }
                    }
                });
                //删除事件
                var w = function (c, i) {
                    if (i.getId().substring(0, 4) == "rect") {
                        flow.rectarr[i.getId()] = null;
                        i.remove();
                    } else {
                        if (i.getId().substring(0, 4) == "path") {
                            flow.patharr[i.getId()] = null;
                            i.remove();
                        }
                    }
                };
                $(pager).bind("removepath", w);
                $(pager).bind("removerect", w);
                //初始化
                var z = {};
                if (d) {
                    if (d.data.rects) {
                        for (var s in d.data.rects) {
                            if (steps && steps.includes(d.data.rects[s].data.id)) d.data.rects[s].data.dealed = true;
                            var r = new flow.rect(d.data.rects[s]);
                            z[d.data.rects[s].data.id] = r;
                            flow.rectarr[r.getId()] = r;
                        }
                    }
                    if (d.data.paths) {
                        for (var s in d.data.paths) {
                            if (steps && steps.includes(d.data.paths[s].from.toString()) && steps.includes(d.data.paths[s].to.toString())) d.data.paths[s].data.dealed = true;
                            var n = new flow.path(z[d.data.paths[s].from], z[d.data.paths[s].to], d.data.paths[s]);
                            flow.patharr[n.getId()] = n;
                        }
                    }
                }

                //获取表单数据
                $.ajax({
                    url: "/FormDesigner/FormDesign/GetFormList",    //后台数据请求地址
                    type: "get",
                    data: { page: 1, limit: 100 },
                    async: false,
                    success: function (slt) {
                        if (slt) {
                            var data = JSON.parse(slt);
                            flow.forms = data.data;
                        }
                        else {
                            //layer.msg(slt.message || '操作失败，请重试。');
                        }
                    }
                });

                //获取职位数据
                $.ajax({
                    url: "/System/Position/GetPositionList",    //后台数据请求地址
                    type: "get",
                    async: false,
                    success: function (slt) {
                        if (slt) {
                            var data = JSON.parse(slt);
                            flow.positions = data.data;
                        }
                        else {
                            //layer.msg(slt.message || '操作失败，请重试。');
                        }
                    }
                });

                //获取用户数据
                $.ajax({
                    url: "/System/User/GetUserList",    //后台数据请求地址
                    type: "get",
                    data: { page: 1, limit: 500 },
                    async: false,
                    success: function (slt) {
                        if (slt) {
                            var data = JSON.parse(slt);
                            flow.users = data.data;
                        }
                        else {
                            //layer.msg(slt.message || '操作失败，请重试。');
                        }
                    }
                });
            };
            flow.resetForm = function (formName) {
                $.ajax({
                    url: "/FormDesigner/FormDesign/GetFormList",    //后台数据请求地址
                    type: "get",
                    data: { page: 1, limit: 100 },
                    async: false,
                    success: function (slt) {
                        if (slt) {
                            var data = JSON.parse(slt);
                            flow.forms = data.data;
                            var pid = $("#pid").val();
                            for (let index = 0; index < flow.forms.length; index++) {
                                const element = flow.forms[index];
                                if (element.FormName == formName) {
                                    flow.rectarr[pid].setattr('formId', element.Id);
                                }
                            }
                            //加载元素属性
                            flow.util.getRectPropertie(flow.rectarr[pid]);
                        }
                        else {
                            //layer.msg(slt.message || '操作失败，请重试。');
                        }
                    }
                });
                $("")
            }
            return flow;
        }
    };
    $.Flow = Flow;
})(jQuery);