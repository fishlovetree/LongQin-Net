USE [longqin]
GO
/****** Object:  Table [dbo].[des_form]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[des_form](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[jsonData] [varchar](max) NOT NULL,
	[tableName] [varchar](50) NOT NULL,
	[formName] [varchar](50) NOT NULL,
	[creator] [int] NULL,
	[organizationId] [int] NULL,
	[createTime] [datetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[isApproval] [tinyint] NULL,
 CONSTRAINT [PK_des_form] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表单json' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_form', @level2type=N'COLUMN',@level2name=N'jsonData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_form', @level2type=N'COLUMN',@level2name=N'tableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_form', @level2type=N'COLUMN',@level2name=N'formName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_form', @level2type=N'COLUMN',@level2name=N'createTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否审批：1-是，0-否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_form', @level2type=N'COLUMN',@level2name=N'isApproval'
GO
SET IDENTITY_INSERT [dbo].[des_form] ON
INSERT [dbo].[des_form] ([id], [jsonData], [tableName], [formName], [creator], [organizationId], [createTime], [status], [isApproval]) VALUES (1, N'[
    {
        "index": 0,
        "tag": "radio",
        "name": "approvalStatus",
        "label": "审批意见",
        "labelwidth": 110,
        "width": 100,
        "disabled": false,
        "labelhide": false,
        "options": [
            {
                "title": "是",
                "value": "1",
                "checked": true
            },
            {
                "title": "否",
                "value": "0",
                "checked": false
            }
        ]
    },
    {
        "index": 1,
        "tag": "textarea",
        "name": "details",
        "label": "意见明细",
        "placeholder": "请输入",
        "default": "",
        "maxlength": "",
        "labelwidth": 110,
        "width": 100,
        "required": false,
        "readonly": false,
        "disabled": false,
        "labelhide": false
    }
]', N'des_approval', N'默认审批表单', 18, 0, CAST(0x0000AFE800A3524B AS DateTime), 1, 1)
INSERT [dbo].[des_form] ([id], [jsonData], [tableName], [formName], [creator], [organizationId], [createTime], [status], [isApproval]) VALUES (21, N'[
    {
        "index": 0,
        "tag": "select",
        "name": "select_0",
        "label": "请假类型",
        "labelwidth": 110,
        "width": 100,
        "lay_search": false,
        "disabled": false,
        "required": true,
        "labelhide": false,
        "options": [
            {
                "title": "事假",
                "value": "1",
                "checked": false
            },
            {
                "title": "病假",
                "value": "2",
                "checked": true
            },
            {
                "title": "年休假",
                "value": "3",
                "checked": false
            },
            {
                "title": "调休",
                "value": "4",
                "checked": false
            },
            {
                "title": "婚假",
                "value": "5",
                "checked": false
            },
            {
                "title": "产假",
                "value": "6",
                "checked": false
            },
            {
                "title": "丧假",
                "value": "7",
                "checked": false
            },
            {
                "title": "护产假",
                "value": "8",
                "checked": false
            }
        ]
    },
    {
        "index": 1,
        "tag": "date",
        "name": "date_1",
        "label": "开始时间",
        "labelwidth": 110,
        "width": 100,
        "data_datetype": "datetime",
        "data_dateformat": "yyyy-MM-dd HH:mm:ss",
        "placeholder": "yyyy-MM-dd HH:mm:ss",
        "data_maxvalue": "9999-12-31",
        "data_minvalue": "1900-01-01",
        "data_range": false,
        "readonly": false,
        "disabled": false,
        "required": true,
        "labelhide": false,
        "default": ""
    },
    {
        "index": 2,
        "tag": "date",
        "name": "date_2",
        "label": "结束时间",
        "labelwidth": 110,
        "width": 100,
        "data_datetype": "datetime",
        "data_dateformat": "yyyy-MM-dd HH:mm:ss",
        "placeholder": "yyyy-MM-dd HH:mm:ss",
        "data_maxvalue": "9999-12-31",
        "data_minvalue": "1900-01-01",
        "data_range": false,
        "readonly": false,
        "disabled": false,
        "required": true,
        "labelhide": false,
        "default": ""
    },
    {
        "index": 4,
        "tag": "input",
        "label": "请假时长",
        "name": "input_4",
        "type": "number",
        "placeholder": "请输入",
        "default": "1",
        "labelwidth": "110",
        "width": 100,
        "maxlength": "",
        "min": "0",
        "max": "100",
        "required": true,
        "readonly": false,
        "disabled": false,
        "labelhide": false,
        "verify": "number"
    },
    {
        "index": 3,
        "tag": "input",
        "label": "事由",
        "name": "input_3",
        "type": "text",
        "placeholder": "请输入事由",
        "default": "",
        "labelwidth": "110",
        "width": 100,
        "maxlength": "",
        "min": 0,
        "max": 0,
        "required": true,
        "readonly": false,
        "disabled": false,
        "labelhide": false,
        "verify": ""
    }
]', N'des_qjsq_2_1', N'请假申请单', 2, 1, CAST(0x0000B06400FD158E AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[des_form] OFF
/****** Object:  Table [dbo].[des_approval]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[des_approval](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[approvalStatus] [varchar](50) NULL,
	[details] [varchar](100) NOT NULL,
	[creator] [int] NULL,
	[createTime] [datetime] NULL,
	[organizationId] [int] NULL,
	[status] [tinyint] NULL,
 CONSTRAINT [PK_des_approval] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审批意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_approval', @level2type=N'COLUMN',@level2name=N'approvalStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'意见明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_approval', @level2type=N'COLUMN',@level2name=N'details'
GO
SET IDENTITY_INSERT [dbo].[des_approval] ON
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (3, N'1', N'同意', 5, CAST(0x0000B06500AF6464 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (4, N'1', N'同意', 7, CAST(0x0000B06500B09190 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (5, N'1', N'同意', 5, CAST(0x0000B06500B0F52C AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (6, N'0', N'不同意', 7, CAST(0x0000B06500B16228 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (8, N'1', N'同意', 5, CAST(0x0000B06500D9A5F8 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (9, N'0', N'不同意', 7, CAST(0x0000B06500E4FF0C AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (10, N'1', N'同意', 5, CAST(0x0000B06500E563D4 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (11, N'1', N'同意', 4, CAST(0x0000B06500E5950C AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (12, N'1', N'同意', 7, CAST(0x0000B06500E5AA24 AS DateTime), 1, 1)
INSERT [dbo].[des_approval] ([id], [approvalStatus], [details], [creator], [createTime], [organizationId], [status]) VALUES (13, N'1', N'同意', 3, CAST(0x0000B06500F79BE4 AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[des_approval] OFF
/****** Object:  Table [dbo].[wf_workform]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_workform](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[workId] [int] NOT NULL,
	[processId] [int] NOT NULL,
	[nodeId] [int] NOT NULL,
	[tableName] [varchar](50) NULL,
	[formDataId] [int] NOT NULL,
 CONSTRAINT [PK_wf_formdata] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[wf_workform] ON
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (1, 1, 1, 1, N'des_qjsq_2_1', 1)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (4, 1, 2, 3, N'des_approval', 3)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (5, 1, 3, 5, N'des_approval', 4)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (7, 2, 5, 3, N'des_approval', 5)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (8, 2, 6, 5, N'des_approval', 6)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (11, 2, 8, 3, N'des_approval', 8)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (12, 2, 9, 5, N'des_approval', 9)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (13, 2, 10, 1, N'des_qjsq_2_1', 2)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (14, 2, 11, 3, N'des_approval', 10)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (15, 2, 12, 2, N'des_approval', 11)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (16, 2, 13, 5, N'des_approval', 12)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (17, 3, 14, 1, N'des_qjsq_2_1', 3)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (18, 3, 15, 3, N'des_approval', 13)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (19, 4, 17, 1, N'des_qjsq_2_1', 4)
INSERT [dbo].[wf_workform] ([id], [workId], [processId], [nodeId], [tableName], [formDataId]) VALUES (20, 5, 19, 1, N'des_qjsq_2_1', 5)
SET IDENTITY_INSERT [dbo].[wf_workform] OFF
/****** Object:  Table [dbo].[wf_work]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wf_work](
	[workId] [int] IDENTITY(1,1) NOT NULL,
	[flowId] [int] NOT NULL,
	[creator] [int] NOT NULL,
	[createTime] [datetime] NOT NULL,
	[closeTime] [datetime] NULL,
	[status] [tinyint] NOT NULL,
	[organizationId] [int] NULL,
 CONSTRAINT [PK_wf_work] PRIMARY KEY CLUSTERED 
(
	[workId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_work', @level2type=N'COLUMN',@level2name=N'flowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_work', @level2type=N'COLUMN',@level2name=N'creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-运行中，0-关闭，9-作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_work', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_work', @level2type=N'COLUMN',@level2name=N'organizationId'
GO
SET IDENTITY_INSERT [dbo].[wf_work] ON
INSERT [dbo].[wf_work] ([workId], [flowId], [creator], [createTime], [closeTime], [status], [organizationId]) VALUES (1, 1, 6, CAST(0x0000B06500ADDB36 AS DateTime), CAST(0x0000B06500B09015 AS DateTime), 0, 1)
INSERT [dbo].[wf_work] ([workId], [flowId], [creator], [createTime], [closeTime], [status], [organizationId]) VALUES (2, 1, 6, CAST(0x0000B06500B0D648 AS DateTime), CAST(0x0000B06500E5A5B6 AS DateTime), 0, 1)
INSERT [dbo].[wf_work] ([workId], [flowId], [creator], [createTime], [closeTime], [status], [organizationId]) VALUES (3, 1, 4, CAST(0x0000B06500F77568 AS DateTime), NULL, 1, 1)
INSERT [dbo].[wf_work] ([workId], [flowId], [creator], [createTime], [closeTime], [status], [organizationId]) VALUES (4, 1, 6, CAST(0x0000B066011C5E99 AS DateTime), NULL, 1, 1)
INSERT [dbo].[wf_work] ([workId], [flowId], [creator], [createTime], [closeTime], [status], [organizationId]) VALUES (5, 1, 6, CAST(0x0000B066016942CF AS DateTime), NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[wf_work] OFF
/****** Object:  Table [dbo].[wf_step]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_step](
	[stepId] [int] IDENTITY(1,1) NOT NULL,
	[workId] [int] NOT NULL,
	[nodeId] [int] NOT NULL,
	[processId] [int] NULL,
	[submitter] [int] NOT NULL,
	[submitTime] [datetime] NOT NULL,
	[action] [tinyint] NOT NULL,
	[reason] [varchar](100) NULL,
	[organizationId] [int] NULL,
 CONSTRAINT [PK_wf_step] PRIMARY KEY CLUSTERED 
(
	[stepId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'workId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'nodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'submitter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'submitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作：1-前进，2-跳转，3-转办' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_step', @level2type=N'COLUMN',@level2name=N'reason'
GO
SET IDENTITY_INSERT [dbo].[wf_step] ON
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (1, 1, 1, 1, 6, CAST(0x0000B06500ADDB70 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (4, 1, 3, 2, 5, CAST(0x0000B06500AF62BC AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (5, 1, 5, 3, 7, CAST(0x0000B06500B08FE6 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (6, 2, 1, 4, 6, CAST(0x0000B06500B0D82A AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (7, 2, 3, 5, 5, CAST(0x0000B06500B0F1A2 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (8, 2, 5, 6, 7, CAST(0x0000B06500B15E4E AS DateTime), 2, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (9, 2, 1, 7, 6, CAST(0x0000B06500B1D509 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (11, 2, 3, 8, 5, CAST(0x0000B06500D9A97A AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (12, 2, 5, 9, 7, CAST(0x0000B06500E4F9E0 AS DateTime), 2, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (13, 2, 1, 10, 6, CAST(0x0000B06500E54485 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (14, 2, 3, 11, 5, CAST(0x0000B06500E55F71 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (15, 2, 2, 12, 4, CAST(0x0000B06500E59061 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (16, 2, 5, 13, 7, CAST(0x0000B06500E5A591 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (17, 3, 1, 14, 4, CAST(0x0000B06500F7759A AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (18, 3, 3, 15, 3, CAST(0x0000B06500F797BC AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (19, 4, 1, 17, 6, CAST(0x0000B066011C5EA2 AS DateTime), 1, NULL, 1)
INSERT [dbo].[wf_step] ([stepId], [workId], [nodeId], [processId], [submitter], [submitTime], [action], [reason], [organizationId]) VALUES (20, 5, 1, 19, 6, CAST(0x0000B066016942FC AS DateTime), 1, NULL, 1)
SET IDENTITY_INSERT [dbo].[wf_step] OFF
/****** Object:  Table [dbo].[wf_sort]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_sort](
	[sortid] [int] IDENTITY(1,1) NOT NULL,
	[sortname] [varchar](50) NOT NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_wf_sort] PRIMARY KEY CLUSTERED 
(
	[sortid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_sort', @level2type=N'COLUMN',@level2name=N'sortname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_sort', @level2type=N'COLUMN',@level2name=N'status'
GO
/****** Object:  Table [dbo].[wf_process]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wf_process](
	[processId] [int] IDENTITY(1,1) NOT NULL,
	[workId] [int] NOT NULL,
	[nodeId] [int] NOT NULL,
	[linkId] [int] NULL,
	[sendingTo] [int] NOT NULL,
	[processType] [tinyint] NULL,
	[submitter] [int] NOT NULL,
	[submitTime] [datetime] NOT NULL,
	[flag] [tinyint] NOT NULL,
	[status] [tinyint] NOT NULL,
	[organizationId] [int] NULL,
 CONSTRAINT [PK_wf_process] PRIMARY KEY CLUSTERED 
(
	[processId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作进程id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'processId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实例id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'workId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'nodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连线id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'linkId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作接收人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'sendingTo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-主送，2-抄送' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'processType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'submitter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'submitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-未读，2-已读' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-运行中，0-关闭，2-就绪' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_process', @level2type=N'COLUMN',@level2name=N'status'
GO
SET IDENTITY_INSERT [dbo].[wf_process] ON
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (1, 1, 1, 0, 6, 1, 6, CAST(0x0000B06500ADDB4D AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (2, 1, 3, 37, 5, 1, 6, CAST(0x0000B06500ADDBD2 AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (3, 1, 5, 40, 7, 1, 5, CAST(0x0000B06500AFE3ED AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (4, 2, 1, 0, 6, 1, 6, CAST(0x0000B06500B0D652 AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (5, 2, 3, 37, 5, 1, 6, CAST(0x0000B06500B0D969 AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (6, 2, 5, 40, 7, 1, 5, CAST(0x0000B06500B10D3D AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (7, 2, 1, 0, 6, 1, 6, CAST(0x0000B06500B15E76 AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (8, 2, 3, 37, 5, 1, 6, CAST(0x0000B06500B1D6D9 AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (9, 2, 5, 40, 7, 1, 5, CAST(0x0000B06500D9D1A5 AS DateTime), 1, 9, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (10, 2, 1, 0, 6, 1, 6, CAST(0x0000B06500E4FA06 AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (11, 2, 3, 43, 5, 1, 6, CAST(0x0000B06500E544DB AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (12, 2, 2, 47, 4, 1, 5, CAST(0x0000B06500E5600E AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (13, 2, 5, 48, 7, 1, 4, CAST(0x0000B06500E590A4 AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (14, 3, 1, 0, 4, 1, 4, CAST(0x0000B06500F77574 AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (15, 3, 3, 43, 3, 1, 4, CAST(0x0000B06500F77624 AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (16, 3, 5, 46, 7, 1, 3, CAST(0x0000B06500F79847 AS DateTime), 1, 1, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (17, 4, 1, 0, 6, 1, 6, CAST(0x0000B066011C5E9B AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (18, 4, 3, 43, 5, 1, 6, CAST(0x0000B066011C5EAF AS DateTime), 1, 1, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (19, 5, 1, 0, 6, 1, 6, CAST(0x0000B066016942DA AS DateTime), 1, 0, 1)
INSERT [dbo].[wf_process] ([processId], [workId], [nodeId], [linkId], [sendingTo], [processType], [submitter], [submitTime], [flag], [status], [organizationId]) VALUES (20, 5, 3, 43, 5, 1, 6, CAST(0x0000B0660169435F AS DateTime), 1, 1, 1)
SET IDENTITY_INSERT [dbo].[wf_process] OFF
/****** Object:  Table [dbo].[wf_node]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_node](
	[nodeId] [int] IDENTITY(1,1) NOT NULL,
	[nodeName] [varchar](50) NOT NULL,
	[nodeType] [tinyint] NOT NULL,
	[formId] [int] NOT NULL,
	[groupseq] [int] NULL,
	[virtual] [tinyint] NULL,
	[cooperation] [tinyint] NULL,
	[positionX] [int] NULL,
	[positionY] [int] NULL,
	[description] [varchar](100) NULL,
	[status] [tinyint] NOT NULL,
	[flowId] [int] NOT NULL,
	[creator] [int] NULL,
	[organizationId] [int] NULL,
	[createTime] [datetime] NOT NULL,
	[departmentId] [int] NULL,
	[positionId] [int] NULL,
	[userId] [int] NULL,
	[isApproval] [tinyint] NULL,
 CONSTRAINT [PK_wf_node] PRIMARY KEY CLUSTERED 
(
	[nodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'nodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点类型：1-普通节点，2-分流节点，3-合流节点，4-分合流点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'nodeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主送表单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'formId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'groupseq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否虚拟节点：1-是，0-否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'virtual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否多人协作：1-是，0-否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'cooperation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点位置：x轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'positionX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点位置：y轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'positionY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'节点描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属流程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'flowId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否审批：1-是，0-否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_node', @level2type=N'COLUMN',@level2name=N'isApproval'
GO
SET IDENTITY_INSERT [dbo].[wf_node] ON
INSERT [dbo].[wf_node] ([nodeId], [nodeName], [nodeType], [formId], [groupseq], [virtual], [cooperation], [positionX], [positionY], [description], [status], [flowId], [creator], [organizationId], [createTime], [departmentId], [positionId], [userId], [isApproval]) VALUES (1, N'请假申请', 0, 21, 1, 0, 0, 301, 100, N'', 1, 1, 2, 1, CAST(0x0000B0640101C180 AS DateTime), 0, 0, 0, 0)
INSERT [dbo].[wf_node] ([nodeId], [nodeName], [nodeType], [formId], [groupseq], [virtual], [cooperation], [positionX], [positionY], [description], [status], [flowId], [creator], [organizationId], [createTime], [departmentId], [positionId], [userId], [isApproval]) VALUES (2, N'请假单上级领导审核', 0, 1, 2, 0, 0, 68, 220, N'', 1, 1, 2, 1, CAST(0x0000B0640101C181 AS DateTime), 0, 0, 0, 0)
INSERT [dbo].[wf_node] ([nodeId], [nodeName], [nodeType], [formId], [groupseq], [virtual], [cooperation], [positionX], [positionY], [description], [status], [flowId], [creator], [organizationId], [createTime], [departmentId], [positionId], [userId], [isApproval]) VALUES (3, N'请假单部门领导审核', 0, 1, 2, 0, 0, 301, 225, N'', 1, 1, 2, 1, CAST(0x0000B0640101C182 AS DateTime), 0, 0, 0, 0)
INSERT [dbo].[wf_node] ([nodeId], [nodeName], [nodeType], [formId], [groupseq], [virtual], [cooperation], [positionX], [positionY], [description], [status], [flowId], [creator], [organizationId], [createTime], [departmentId], [positionId], [userId], [isApproval]) VALUES (4, N'请假单上级领导审核', 0, 1, 2, 0, 0, 503, 227, N'', 1, 1, 2, 1, CAST(0x0000B0640101C184 AS DateTime), 0, 0, 0, 0)
INSERT [dbo].[wf_node] ([nodeId], [nodeName], [nodeType], [formId], [groupseq], [virtual], [cooperation], [positionX], [positionY], [description], [status], [flowId], [creator], [organizationId], [createTime], [departmentId], [positionId], [userId], [isApproval]) VALUES (5, N'人事审核', 0, 1, 9, 0, 0, 300, 407, N'', 1, 1, 2, 1, CAST(0x0000B0640101C186 AS DateTime), 0, 0, 7, 0)
SET IDENTITY_INSERT [dbo].[wf_node] OFF
/****** Object:  Table [dbo].[wf_link]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_link](
	[linkId] [int] IDENTITY(1,1) NOT NULL,
	[linkName] [varchar](50) NOT NULL,
	[fromNodeId] [int] NOT NULL,
	[toNodeId] [int] NOT NULL,
	[formId] [int] NULL,
	[field] [varchar](50) NULL,
	[operator] [varchar](10) NULL,
	[operatorValue] [varchar](50) NULL,
	[positionX] [int] NULL,
	[positionY] [int] NULL,
	[description] [varchar](100) NULL,
	[status] [tinyint] NOT NULL,
	[flowId] [int] NOT NULL,
	[createTime] [datetime] NOT NULL,
	[creator] [int] NULL,
	[organizationId] [int] NULL,
 CONSTRAINT [PK_wf_link] PRIMARY KEY CLUSTERED 
(
	[linkId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连线名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'linkName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'起始节点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'fromNodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'终到节点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'toNodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条件字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'field'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较符号：><=等' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'比较的基准值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'operatorValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连线位置：x轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'positionX'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连线位置：y轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'positionY'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'连线描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属流程id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_link', @level2type=N'COLUMN',@level2name=N'flowId'
GO
SET IDENTITY_INSERT [dbo].[wf_link] ON
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (43, N'基层员工', 1, 3, 0, N'positionLevel', N'=', N'', 0, -10, N'', 1, 1, CAST(0x0000B06500DC9069 AS DateTime), 2, 1)
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (44, N'中高层员工', 1, 4, 0, N'positionLevel', N'!=', N'', 0, -10, N'', 1, 1, CAST(0x0000B06500DC9073 AS DateTime), 2, 1)
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (45, N'人事审核', 4, 5, 0, N'', N'', N'', 0, -10, N'', 1, 1, CAST(0x0000B06500DC907D AS DateTime), 2, 1)
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (46, N'三天以内', 3, 5, 21, N'input_4', N'<=', N'3', 0, -10, N'', 1, 1, CAST(0x0000B06500DC9086 AS DateTime), 2, 1)
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (47, N'大于三天', 3, 2, 21, N'input_4', N'>', N'3', 0, -10, N'', 1, 1, CAST(0x0000B06500DC9090 AS DateTime), 2, 1)
INSERT [dbo].[wf_link] ([linkId], [linkName], [fromNodeId], [toNodeId], [formId], [field], [operator], [operatorValue], [positionX], [positionY], [description], [status], [flowId], [createTime], [creator], [organizationId]) VALUES (48, N'人事审核', 2, 5, 0, N'', N'', N'', 0, -10, N'', 1, 1, CAST(0x0000B06500DC9099 AS DateTime), 2, 1)
SET IDENTITY_INSERT [dbo].[wf_link] OFF
/****** Object:  Table [dbo].[wf_label]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_label](
	[labelid] [int] IDENTITY(1,1) NOT NULL,
	[labeltext] [varchar](50) NOT NULL,
	[positionx] [int] NULL,
	[positiony] [int] NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_wf_label] PRIMARY KEY CLUSTERED 
(
	[labelid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标签文字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_label', @level2type=N'COLUMN',@level2name=N'labeltext'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标签位置：y轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_label', @level2type=N'COLUMN',@level2name=N'positionx'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标签位置：y轴' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_label', @level2type=N'COLUMN',@level2name=N'positiony'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_label', @level2type=N'COLUMN',@level2name=N'status'
GO
/****** Object:  Table [dbo].[wf_flow]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[wf_flow](
	[flowId] [int] IDENTITY(1,1) NOT NULL,
	[flowName] [varchar](50) NOT NULL,
	[flowSort] [int] NULL,
	[description] [varchar](100) NULL,
	[status] [tinyint] NOT NULL,
	[flowParam] [varchar](50) NULL,
	[organizationId] [int] NULL,
	[creator] [int] NULL,
	[createTime] [datetime] NOT NULL,
 CONSTRAINT [PK_wf_flow] PRIMARY KEY CLUSTERED 
(
	[flowId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_flow', @level2type=N'COLUMN',@level2name=N'flowName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_flow', @level2type=N'COLUMN',@level2name=N'flowSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_flow', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_flow', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_flow', @level2type=N'COLUMN',@level2name=N'flowParam'
GO
SET IDENTITY_INSERT [dbo].[wf_flow] ON
INSERT [dbo].[wf_flow] ([flowId], [flowName], [flowSort], [description], [status], [flowParam], [organizationId], [creator], [createTime]) VALUES (1, N'请假流程', 1, NULL, 1, NULL, 1, 2, CAST(0x0000B0640101C179 AS DateTime))
SET IDENTITY_INSERT [dbo].[wf_flow] OFF
/****** Object:  Table [dbo].[wf_entrustmx]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wf_entrustmx](
	[entrustid] [int] NOT NULL,
	[flowid] [int] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'委托id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrustmx', @level2type=N'COLUMN',@level2name=N'entrustid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrustmx', @level2type=N'COLUMN',@level2name=N'flowid'
GO
/****** Object:  Table [dbo].[wf_entrust]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wf_entrust](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entruster] [int] NOT NULL,
	[trustee] [int] NOT NULL,
	[alltime] [tinyint] NOT NULL,
	[begintime] [datetime] NULL,
	[endtime] [datetime] NULL,
	[createtime] [datetime] NULL,
	[status] [tinyint] NOT NULL,
 CONSTRAINT [PK_wf_entrust] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'委托人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'entruster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被委托人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'trustee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-一直有效，0-否' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'alltime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'begintime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'endtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'createtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wf_entrust', @level2type=N'COLUMN',@level2name=N'status'
GO
/****** Object:  Table [dbo].[sys_userrole]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_userrole](
	[userId] [int] NOT NULL,
	[roleId] [int] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_userrole', @level2type=N'COLUMN',@level2name=N'userId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_userrole', @level2type=N'COLUMN',@level2name=N'roleId'
GO
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (2, 1)
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (3, 1)
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (4, 2)
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (5, 2)
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (6, 2)
INSERT [dbo].[sys_userrole] ([userId], [roleId]) VALUES (7, 2)
/****** Object:  Table [dbo].[sys_user]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_user](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NOT NULL,
	[password] [varchar](100) NULL,
	[nickName] [varchar](50) NULL,
	[avatar] [varchar](100) NULL,
	[departmentId] [int] NULL,
	[description] [varchar](100) NULL,
	[createTime] [datetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[organizationId] [int] NULL,
	[positionId] [int] NULL,
	[email] [varchar](50) NULL,
	[phone] [varchar](25) NULL,
 CONSTRAINT [PK_sys_user1] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'positionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_user', @level2type=N'COLUMN',@level2name=N'phone'
GO
SET IDENTITY_INSERT [dbo].[sys_user] ON
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (1, N'admin', N'1bbd886460827015e5d605ed44252251', N'超级管理员', NULL, 0, NULL, CAST(0x0000B06401575FA6 AS DateTime), 1, 0, 0, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (2, N'custom', N'1bbd886460827015e5d605ed44252251', N'客户甲', N'/content/upload/images/987d4849-f11c-4641-9e36-d33c6327acff20230820150442.png', 0, NULL, CAST(0x0000B06400F44992 AS DateTime), 1, 1, 0, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (3, N'zongjingli', N'be2f70b37e254161d92ce1a7a90946f1', N'总经理', N'/content/upload/images/9f50b0f2-a3f2-48bb-8b75-c7c9c2a3a09420230820150607.png', 1, NULL, CAST(0x0000B06400F8DF73 AS DateTime), 1, 1, 1, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (4, N'songjiang', N'8f8f121168bd1ab852716d1eb75fe486', N'宋江', N'/content/upload/images/0febaa8d-384f-4192-9a52-7b90208b61c520230820150938.png', 2, NULL, CAST(0x0000B06400F9D778 AS DateTime), 1, 1, 2, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (5, N'wusong', N'93fa40dc579e57d9baa812a030d6bf39', N'武松', N'/content/upload/images/908b4ee7-872e-442c-921c-6f35685094bf20230820151041.png', 3, NULL, CAST(0x0000B06400FA218B AS DateTime), 1, 1, 4, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (6, N'zhangqing', N'f263871bc224eafa6dcfc2e2522d4549', N'张青', N'/content/upload/images/1eef86f7-4bd7-44b4-9a3c-350990fef2fc20230820151309.png', 3, NULL, CAST(0x0000B06400FACE32 AS DateTime), 1, 1, 7, N'heyunche@sina.com', N'13616526044')
INSERT [dbo].[sys_user] ([userId], [userName], [password], [nickName], [avatar], [departmentId], [description], [createTime], [status], [organizationId], [positionId], [email], [phone]) VALUES (7, N'renshi', N'd4cf080c117364277c0e216dec2cfa2c', N'人事1', N'/content/upload/images/2ee7dfef-6744-4af8-be29-cdd99a22927920230820153146.png', 8, NULL, CAST(0x0000B06400FFEAEB AS DateTime), 1, 1, 11, N'heyunche@sina.com', N'13616526044')
SET IDENTITY_INSERT [dbo].[sys_user] OFF
/****** Object:  Table [dbo].[sys_rolemenu]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sys_rolemenu](
	[roleId] [int] NOT NULL,
	[menuId] [int] NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_rolemenu', @level2type=N'COLUMN',@level2name=N'roleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_rolemenu', @level2type=N'COLUMN',@level2name=N'menuId'
GO
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 18)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 19)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 20)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 15)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 16)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 17)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 21)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 8)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 10)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 9)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 11)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 14)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 12)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 1)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 6)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 5)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 13)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 18)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 19)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 15)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 16)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 17)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 21)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 11)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 14)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 1)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 3)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (1, 7)
INSERT [dbo].[sys_rolemenu] ([roleId], [menuId]) VALUES (2, 7)
/****** Object:  Table [dbo].[sys_role]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL,
	[description] [varchar](100) NULL,
	[status] [tinyint] NOT NULL,
	[createTime] [datetime] NULL,
	[organizationId] [int] NULL,
 CONSTRAINT [PK_sys_role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'roleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_role', @level2type=N'COLUMN',@level2name=N'status'
GO
SET IDENTITY_INSERT [dbo].[sys_role] ON
INSERT [dbo].[sys_role] ([roleId], [roleName], [description], [status], [createTime], [organizationId]) VALUES (1, N'管理员', NULL, 1, CAST(0x0000B06400F3E369 AS DateTime), 1)
INSERT [dbo].[sys_role] ([roleId], [roleName], [description], [status], [createTime], [organizationId]) VALUES (2, N'普通员工', NULL, 1, CAST(0x0000B06400F778C6 AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[sys_role] OFF
/****** Object:  Table [dbo].[sys_position]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_position](
	[positionId] [int] IDENTITY(1,1) NOT NULL,
	[positionName] [varchar](50) NOT NULL,
	[parentId] [int] NOT NULL,
	[positionLevel] [tinyint] NULL,
	[description] [varchar](100) NULL,
	[status] [tinyint] NOT NULL,
	[organizationId] [int] NULL,
	[createTime] [datetime] NULL,
 CONSTRAINT [PK_sys_post] PRIMARY KEY CLUSTERED 
(
	[positionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'positionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'positionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级职位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'parentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位等级：1-基层，2-中层，3-高层' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'positionLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_position', @level2type=N'COLUMN',@level2name=N'status'
GO
SET IDENTITY_INSERT [dbo].[sys_position] ON
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (1, N'总经理', 0, 3, NULL, 1, 1, CAST(0x0000B06400F7B7E2 AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (2, N'研发中心主任', 1, 2, NULL, 1, 1, CAST(0x0000B06400F7CF2F AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (3, N'营销中心主任', 1, 2, NULL, 1, 1, CAST(0x0000B06400F7DEB7 AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (4, N'开发部长', 2, 2, NULL, 1, 1, CAST(0x0000B06400F7F33D AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (5, N'测试部长', 2, 2, NULL, 1, 1, CAST(0x0000B06400F801CC AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (6, N'营销组长', 3, 2, NULL, 1, 1, CAST(0x0000B06400F82D2B AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (7, N'开发人员', 4, 1, NULL, 1, 1, CAST(0x0000B06400F84A35 AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (8, N'测试人员', 5, 1, NULL, 1, 1, CAST(0x0000B06400F855C6 AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (9, N'营销人员', 6, 1, NULL, 1, 1, CAST(0x0000B06400F8636B AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (10, N'人力资源部长', 1, 2, NULL, 1, 1, CAST(0x0000B06400FFAC0A AS DateTime))
INSERT [dbo].[sys_position] ([positionId], [positionName], [parentId], [positionLevel], [description], [status], [organizationId], [createTime]) VALUES (11, N'人力资源', 10, 1, NULL, 1, 1, CAST(0x0000B06400FFC86B AS DateTime))
SET IDENTITY_INSERT [dbo].[sys_position] OFF
/****** Object:  Table [dbo].[sys_organization]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_organization](
	[organizationId] [int] IDENTITY(1,1) NOT NULL,
	[organizationCode] [varchar](50) NULL,
	[organizationName] [varchar](100) NULL,
	[parentId] [int] NULL,
	[status] [tinyint] NULL,
	[address] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[logoPath] [varchar](200) NULL,
	[systemName] [varchar](50) NULL,
	[introduction] [varchar](200) NULL,
	[createTime] [datetime] NULL,
 CONSTRAINT [PK_sys_organization] PRIMARY KEY CLUSTERED 
(
	[organizationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[sys_organization] ON
INSERT [dbo].[sys_organization] ([organizationId], [organizationCode], [organizationName], [parentId], [status], [address], [phone], [logoPath], [systemName], [introduction], [createTime]) VALUES (1, N'10001', N'龙琴科技', NULL, 1, N'杭州', N'13616526044', N'/content/upload/images/a08830cc-637d-4a82-a908-4f87946e74ba20230820144738.png', N'龙琴科技', N'龙琴科技', CAST(0x0000B06400F3CC01 AS DateTime))
INSERT [dbo].[sys_organization] ([organizationId], [organizationCode], [organizationName], [parentId], [status], [address], [phone], [logoPath], [systemName], [introduction], [createTime]) VALUES (2, N'10000', N'龙琴工作流系统', NULL, 1, NULL, N'13616526044', NULL, N'龙琴工作流系统', NULL, CAST(0x0000B06601651391 AS DateTime))
SET IDENTITY_INSERT [dbo].[sys_organization] OFF
/****** Object:  Table [dbo].[sys_menu]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_menu](
	[menuId] [int] IDENTITY(1,1) NOT NULL,
	[menuName] [varchar](50) NOT NULL,
	[menuUrl] [varchar](100) NULL,
	[parentId] [int] NOT NULL,
	[groupseq] [int] NULL,
	[menuIcon] [varchar](100) NULL,
	[controller] [varchar](50) NULL,
	[action] [varchar](50) NULL,
	[status] [tinyint] NOT NULL,
	[createTime] [datetime] NULL,
	[organizationId] [int] NULL,
	[creator] [int] NOT NULL,
 CONSTRAINT [PK_sys_menu] PRIMARY KEY CLUSTERED 
(
	[menuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'menuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'menuUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级菜单id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'parentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'groupseq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'menuIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'controller'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'方法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'organizationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_menu', @level2type=N'COLUMN',@level2name=N'creator'
GO
SET IDENTITY_INSERT [dbo].[sys_menu] ON
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (1, N'系统管理', NULL, 0, 5, N'layui-icon-set', NULL, NULL, 1, CAST(0x0000AD270167AE44 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (2, N'菜单管理', N'/system/menu/index', 1, 7, NULL, N'menu', N'index', 1, CAST(0x0000AD2701680E35 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (3, N'用户管理', N'/system/user/index', 1, 5, NULL, N'user', N'index', 1, CAST(0x0000AD2701682A08 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (4, N'公司管理', N'/system/organization/index', 1, 1, NULL, N'organization', N'index', 1, CAST(0x0000AD4801721C46 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (5, N'部门管理', N'/system/department/index', 1, 3, NULL, N'department', N'index', 1, CAST(0x0000AD480175BEF0 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (6, N'角色管理', N'/system/role/index', 1, 2, NULL, N'role', N'index', 1, CAST(0x0000AD4801762212 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (7, N'系统日志', N'/system/log/index', 1, 6, NULL, N'log', N'index', 1, CAST(0x0000AD4801766010 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (8, N'自定义表单', NULL, 0, 2, N'layui-icon-form', NULL, NULL, 1, CAST(0x0000AEFF01169D0C AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (9, N'表单设计器', N'/formdesigner/formdesign/index', 8, 2, NULL, N'FormDesigner', N'Designer', 1, CAST(0x0000AEFF0116D162 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (10, N'表单列表', N'/formdesigner/formdesign/list', 8, 1, NULL, N'Designer', N'List', 1, CAST(0x0000AF30010F3F9A AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (11, N'自定义流程', NULL, 0, 3, N'layui-icon-template-1', NULL, NULL, 1, CAST(0x0000AF32015D793C AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (12, N'流程设计器', N'/flowdesigner/flowdesign/index', 11, 2, NULL, N'FlowDesign', N'Index', 1, CAST(0x0000AF3201621E57 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (13, N'职位管理', N'/system/position/index', 1, 4, NULL, N'Position', N'Index', 1, CAST(0x0000AFE100F19EA2 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (14, N'流程列表', N'/flowdesigner/flowdesign/list', 11, 1, NULL, N'FlowDesign', N'List', 1, CAST(0x0000B00900F1804C AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (15, N'我的工作', NULL, 0, 1, N'layui-icon-app', NULL, NULL, 1, CAST(0x0000B04000FB2F66 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (16, N'待办工作', N'/work/workflow/backlog', 15, 1, NULL, N'WorkFlow', N'Backlog', 1, CAST(0x0000B04000FB7CC2 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (17, N'已办工作', N'/work/workflow/completed', 15, 2, NULL, N'WorkFlow', N'Completed', 1, CAST(0x0000B052015F8D6C AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (18, N'主页', NULL, 0, 0, N'layui-icon-home', NULL, NULL, 1, CAST(0x0000B05E00B27CF8 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (19, N'主页', N'/home/index', 18, 0, NULL, N'home', N'index', 1, CAST(0x0000B05E00B2BFFD AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (20, N'系统设置', N'/system/organization/baseset', 18, 1, NULL, N'organization', N'baseset', 1, CAST(0x0000B0660164153E AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (21, N'流程发起', N'/work/workflow/flowlist', 15, 3, NULL, N'workflow', N'flowlist', 1, CAST(0x0000B0660168F2CF AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (22, N'公告管理', N'/system/notice/index', 18, 3, NULL, N'notice', N'index', 1, CAST(0x0000B06B011740E6 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (23, N'自定义列表', NULL, 0, 4, N'layui-icon-table', NULL, NULL, 1, CAST(0x0000B06F011E20B9 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (24, N'列表清单', N'/tabledesigner/tabledesign/list', 23, 1, NULL, N'tabledesign', N'index', 1, CAST(0x0000B06F011E51A5 AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (25, N'列表设计器', N'/tabledesigner/tabledesign/index', 23, 2, NULL, N'tabledesign', N'index', 1, CAST(0x0000B06F011E9DBC AS DateTime), 0, 1)
INSERT [dbo].[sys_menu] ([menuId], [menuName], [menuUrl], [parentId], [groupseq], [menuIcon], [controller], [action], [status], [createTime], [organizationId], [creator]) VALUES (26, N'请假列表', N'/tabledesigner/tabledesign/customerview/3', 23, 3, NULL, N'tabledesign', N'customerview', 1, CAST(0x0000B07600AB3D15 AS DateTime), 1, 2)
SET IDENTITY_INSERT [dbo].[sys_menu] OFF
/****** Object:  Table [dbo].[sys_log]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_log](
	[logId] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NOT NULL,
	[remark] [varchar](100) NULL,
	[controllerName] [varchar](50) NULL,
	[actionName] [varchar](50) NULL,
	[actionParameters] [varchar](max) NULL,
	[userId] [int] NOT NULL,
	[createTime] [datetime] NULL,
	[organizationId] [int] NOT NULL,
 CONSTRAINT [PK_sys_log] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sys_department]    Script Date: 08/23/2023 12:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_department](
	[departmentId] [int] IDENTITY(1,1) NOT NULL,
	[departmentName] [varchar](50) NOT NULL,
	[parentId] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[description] [varchar](100) NULL,
	[organizationId] [int] NULL,
	[createTime] [datetime] NULL,
 CONSTRAINT [PK_sys_department] PRIMARY KEY CLUSTERED 
(
	[departmentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_department', @level2type=N'COLUMN',@level2name=N'departmentName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级部门id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_department', @level2type=N'COLUMN',@level2name=N'parentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-使用中，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_department', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_department', @level2type=N'COLUMN',@level2name=N'description'
GO
SET IDENTITY_INSERT [dbo].[sys_department] ON
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (1, N'总经理', 0, 1, NULL, 1, CAST(0x0000B06400F6A7D5 AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (2, N'研发中心', 1, 1, NULL, 1, CAST(0x0000B06400F7160B AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (3, N'开发部', 2, 1, NULL, 1, CAST(0x0000B06400F7238D AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (4, N'测试部', 2, 1, NULL, 1, CAST(0x0000B06400F72D37 AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (5, N'营销中心', 1, 1, NULL, 1, CAST(0x0000B06400F73CBE AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (6, N'营销一组', 5, 1, NULL, 1, CAST(0x0000B06400F75B0D AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (7, N'营销二组', 5, 1, NULL, 1, CAST(0x0000B06400F763D4 AS DateTime))
INSERT [dbo].[sys_department] ([departmentId], [departmentName], [parentId], [status], [description], [organizationId], [createTime]) VALUES (8, N'人力资源部', 1, 1, NULL, 1, CAST(0x0000B06400FF92BB AS DateTime))
SET IDENTITY_INSERT [dbo].[sys_department] OFF
/****** Object:  UserDefinedFunction [dbo].[split]    Script Date: 08/23/2023 12:54:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[split]  
(  
    @str1 VARCHAR(MAX), -- 待处理的字符串   
    @sep VARCHAR(10)   -- 分隔字符  
) 
RETURNS @arr TABLE  
(  
    [id] int identity(1,1),
    [value1] VARCHAR(100)
)  
AS  
BEGIN  
    DECLARE @m1 INT,@tmpStr1 VARCHAR(100)
    if @str1 = ''
    begin
    return
    end
    SET @m1 = CHARINDEX(@sep, @str1)
    while @m1 > 0  
        BEGIN
            SET @tmpStr1 = substring(@str1, 1, @m1-1)  
            SET @str1 = STUFF(@str1, 1, @m1, '')  --删除指定长度的字符并在指定的起始点插入另一组字符
            SET @m1 = CHARINDEX(@sep, @str1)  --返回@sep在@str1中的起始位置

            INSERT INTO @arr([value1]) VALUES(@tmpStr1)  
        END  
      INSERT INTO @arr([value1]) VALUES(@str1)    --这里是处理数组的最后一串字符
    RETURN  
END
GO
/****** Object:  UserDefinedFunction [dbo].[getPositionParent]    Script Date: 08/23/2023 12:54:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getPositionParent]  
(  
    @id INT -- 当前职位id    
) 
RETURNS VARCHAR(MAX)
AS  
BEGIN  
    DECLARE @i INT, @res VARCHAR(MAX)
    SET @i = -1
    SET @res = @id
    while @i != 0  
    begin
        SELECT @i = PARENTID FROM sys_position as so WHERE so.positionId = @id
        if @i = -1
        begin
            set @i = 0
        end
        else
        begin
            set @id = @i
	        set @res = convert(varchar,@id) + ',' + @res
        end
    end
	RETURN @res
END
GO
/****** Object:  UserDefinedFunction [dbo].[getDeptParent]    Script Date: 08/23/2023 12:54:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getDeptParent]  
(  
    @id INT -- 当前部门id    
) 
RETURNS VARCHAR(MAX)
AS  
BEGIN  
    DECLARE @i INT, @res VARCHAR(MAX)
    SET @i = -1
    SET @res = @id
    while @i != 0  
    begin
        SELECT @i = PARENTID FROM sys_department as so WHERE so.departmentId = @id
        if @i = -1
        begin
            set @i = 0
        end
        else
        begin
            set @id = @i
	        set @res = convert(varchar,@id) + ',' + @res
        end
    end
	RETURN @res
END
GO
/****** Object:  Default [DF_des_form_intime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[des_form] ADD  CONSTRAINT [DF_des_form_intime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_des_form_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[des_form] ADD  CONSTRAINT [DF_des_form_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_des_form_isApproval]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[des_form] ADD  CONSTRAINT [DF_des_form_isApproval]  DEFAULT ((0)) FOR [isApproval]
GO
/****** Object:  Default [DF_sys_department_parentId]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_department] ADD  CONSTRAINT [DF_sys_department_parentId]  DEFAULT ((-1)) FOR [parentId]
GO
/****** Object:  Default [DF_sys_department_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_department] ADD  CONSTRAINT [DF_sys_department_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_sys_department_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_department] ADD  CONSTRAINT [DF_sys_department_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_log_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_log] ADD  CONSTRAINT [DF_sys_log_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_menu_parentid]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_menu] ADD  CONSTRAINT [DF_sys_menu_parentid]  DEFAULT ((-1)) FOR [parentId]
GO
/****** Object:  Default [DF_sys_menu_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_menu] ADD  CONSTRAINT [DF_sys_menu_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_sys_menu_createtime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_menu] ADD  CONSTRAINT [DF_sys_menu_createtime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_organization_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_organization] ADD  CONSTRAINT [DF_sys_organization_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_sys_organization_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_organization] ADD  CONSTRAINT [DF_sys_organization_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_post_paretnid]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_position] ADD  CONSTRAINT [DF_sys_post_paretnid]  DEFAULT ((-1)) FOR [parentId]
GO
/****** Object:  Default [DF_sys_post_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_position] ADD  CONSTRAINT [DF_sys_post_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_sys_position_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_position] ADD  CONSTRAINT [DF_sys_position_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_role_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_role] ADD  CONSTRAINT [DF_sys_role_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_sys_role_createtime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_role] ADD  CONSTRAINT [DF_sys_role_createtime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_user1_createtime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[sys_user] ADD  CONSTRAINT [DF_sys_user1_createtime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_wf_entrust_alltime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_entrust] ADD  CONSTRAINT [DF_wf_entrust_alltime]  DEFAULT ((1)) FOR [alltime]
GO
/****** Object:  Default [DF_wf_entrust_createtime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_entrust] ADD  CONSTRAINT [DF_wf_entrust_createtime]  DEFAULT (getdate()) FOR [createtime]
GO
/****** Object:  Default [DF_wf_entrust_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_entrust] ADD  CONSTRAINT [DF_wf_entrust_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_flow_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_flow] ADD  CONSTRAINT [DF_wf_flow_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_flow_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_flow] ADD  CONSTRAINT [DF_wf_flow_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_wf_label_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_label] ADD  CONSTRAINT [DF_wf_label_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_link_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_link] ADD  CONSTRAINT [DF_wf_link_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_link_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_link] ADD  CONSTRAINT [DF_wf_link_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_wf_node_virtual]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_node] ADD  CONSTRAINT [DF_wf_node_virtual]  DEFAULT ((0)) FOR [virtual]
GO
/****** Object:  Default [DF_wf_node_cooperation]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_node] ADD  CONSTRAINT [DF_wf_node_cooperation]  DEFAULT ((0)) FOR [cooperation]
GO
/****** Object:  Default [DF_wf_node_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_node] ADD  CONSTRAINT [DF_wf_node_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_node_createTime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_node] ADD  CONSTRAINT [DF_wf_node_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_wf_process_processType]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_process] ADD  CONSTRAINT [DF_wf_process_processType]  DEFAULT ((1)) FOR [processType]
GO
/****** Object:  Default [DF_wf_process_submittime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_process] ADD  CONSTRAINT [DF_wf_process_submittime]  DEFAULT (getdate()) FOR [submitTime]
GO
/****** Object:  Default [DF_wf_process_flag]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_process] ADD  CONSTRAINT [DF_wf_process_flag]  DEFAULT ((1)) FOR [flag]
GO
/****** Object:  Default [DF_wf_process_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_process] ADD  CONSTRAINT [DF_wf_process_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_sort_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_sort] ADD  CONSTRAINT [DF_wf_sort_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_wf_step_submittime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_step] ADD  CONSTRAINT [DF_wf_step_submittime]  DEFAULT (getdate()) FOR [submitTime]
GO
/****** Object:  Default [DF_wf_step_action]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_step] ADD  CONSTRAINT [DF_wf_step_action]  DEFAULT ((1)) FOR [action]
GO
/****** Object:  Default [DF_wf_work_createtime]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_work] ADD  CONSTRAINT [DF_wf_work_createtime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_wf_work_status]    Script Date: 08/23/2023 12:54:47 ******/
ALTER TABLE [dbo].[wf_work] ADD  CONSTRAINT [DF_wf_work_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Table [dbo].[sys_notice_files]    Script Date: 08/30/2023 22:24:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_notice_files](
	[noticeId] [int] NOT NULL,
	[filePath] [varchar](200) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公告主表id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice_files', @level2type=N'COLUMN',@level2name=N'noticeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文档路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice_files', @level2type=N'COLUMN',@level2name=N'filePath'
GO
/****** Object:  Table [dbo].[sys_notice]    Script Date: 08/30/2023 22:24:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sys_notice](
	[noticeId] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NOT NULL,
	[noticeLevel] [tinyint] NULL,
	[security] [tinyint] NULL,
	[createTime] [datetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[organizationId] [int] NULL,
	[creator] [int] NULL,
	[content] [text] NULL,
	[attachments] [varchar](2000) NULL,
 CONSTRAINT [PK_sys_notice] PRIMARY KEY CLUSTERED 
(
	[noticeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'noticeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'紧急程度：1-普通，2-紧急，3-加急' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'noticeLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保密程度：1-公开，2-内部公开，3-机密' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'security'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'createTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：1-可用，0-删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'organizationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发文人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公告内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sys_notice', @level2type=N'COLUMN',@level2name=N'attachments'
GO
/****** Object:  Default [DF_sys_notice_createTime]    Script Date: 08/30/2023 22:24:56 ******/
ALTER TABLE [dbo].[sys_notice] ADD  CONSTRAINT [DF_sys_notice_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_sys_notice_status]    Script Date: 08/30/2023 22:24:56 ******/
ALTER TABLE [dbo].[sys_notice] ADD  CONSTRAINT [DF_sys_notice_status]  DEFAULT ((1)) FOR [status]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[diy_table_columns](
	[tableId] [int] NOT NULL,
	[tableColumn] [varchar](50) NOT NULL,
	[columnName] [varchar](50) NULL,
	[columnIndex] [int] NULL,
	[width] [int] NULL,
	[orderby] [tinyint] NULL,
	[searchType] [tinyint] NULL,
	[formula] [tinyint] NULL,
	[formulaValue] [varchar](50) NULL,
	[columnType] [varchar](50) NULL,
 CONSTRAINT [PK_diy_table_columns] PRIMARY KEY CLUSTERED 
(
	[tableId] ASC,
	[tableColumn] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列表id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'tableId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列表字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'tableColumn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'columnName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'columnIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否排序：0-不排序，1-升序，2-降序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'orderby'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否搜索：0-否，1-等于，2-模糊查询，3-介于' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'searchType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公式：0-否，1-加，2-减，3-乘，4-除，5-拼接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'formula'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公式值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'formulaValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table_columns', @level2type=N'COLUMN',@level2name=N'columnType'
GO
/****** Object:  Table [dbo].[diy_table]    Script Date: 09/10/2023 19:50:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[diy_table](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tableName] [varchar](50) NULL,
	[creator] [int] NULL,
	[organizationId] [int] NULL,
	[createTime] [datetime] NOT NULL,
	[status] [tinyint] NOT NULL,
	[dataSource] [varchar](50) NULL,
 CONSTRAINT [PK_diy_table] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列表名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table', @level2type=N'COLUMN',@level2name=N'tableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table', @level2type=N'COLUMN',@level2name=N'createTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据源表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'diy_table', @level2type=N'COLUMN',@level2name=N'dataSource'
GO
/****** Object:  Default [DF_diy_table_createTime]    Script Date: 09/10/2023 19:50:57 ******/
ALTER TABLE [dbo].[diy_table] ADD  CONSTRAINT [DF_diy_table_createTime]  DEFAULT (getdate()) FOR [createTime]
GO
/****** Object:  Default [DF_diy_table_status]    Script Date: 09/10/2023 19:50:57 ******/
ALTER TABLE [dbo].[diy_table] ADD  CONSTRAINT [DF_diy_table_status]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF_diy_table_columns_orderby]    Script Date: 09/10/2023 19:50:57 ******/
ALTER TABLE [dbo].[diy_table_columns] ADD  CONSTRAINT [DF_diy_table_columns_orderby]  DEFAULT ((0)) FOR [orderby]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[des_qjsq_2_1](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[select_0] [varchar](50) NOT NULL,
	[date_1] [varchar](50) NOT NULL,
	[date_2] [varchar](50) NOT NULL,
	[input_4] [varchar](100) NOT NULL,
	[input_3] [varchar](100) NOT NULL,
	[input_0] [varchar](100) NOT NULL,
	[creator] [int] NULL,
	[createTime] [datetime] NULL,
	[organizationId] [int] NULL,
	[status] [tinyint] NULL,
 CONSTRAINT [PK_des_qjsq_2_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请假类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'select_0'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'date_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'date_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请假时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'input_4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事由' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'input_3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事由2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'des_qjsq_2_1', @level2type=N'COLUMN',@level2name=N'input_0'
GO