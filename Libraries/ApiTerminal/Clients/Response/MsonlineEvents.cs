using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kalendar.Zero.ApiTerminal.Clients.Response
{
    [Serializable]
    [DataContract]
    public class MsonlineEvents
    {
        [DataMember(Name = "value")]
        public List<MsonlineEvent> Data { get; set; }

        [DataMember(Name = "@odata.nextLink")]
        public string NextLink { get; set; }

        /*
         {
    "@odata.context": "https://graph.microsoft.com/v1.0/$metadata#users('zhaoma%40hotmail.com')/calendar/events",
    "value": [
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUILQ==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmcwAAAA==",
            "createdDateTime": "2017-07-30T09:00:11.4592521Z",
            "lastModifiedDateTime": "2017-07-30T09:00:11.4748781Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUILQ==",
            "categories": [],
            "originalStartTimeZone": "China Standard Time",
            "originalEndTimeZone": "China Standard Time",
            "iCalUId": "040000008200E00074C5B7101A82E0080000000009732C401209D301000000000000000010000000C6D3269A5FE98149AD6D703A141427C9",
            "reminderMinutesBeforeStart": 240,
            "isReminderOn": true,
            "hasAttachments": false,
            "subject": "every month 4-5",
            "bodyPreview": "every month 4-5 rr",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": false,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "tentative",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmcwAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=us-ascii\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p><span>every month 4-5 rr</span><br>\r\n</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-07-04T01:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-07-05T04:00:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "every month 4-5 LOC"
            },
            "recurrence": {
                "pattern": {
                    "type": "absoluteMonthly",
                    "interval": 1,
                    "month": 0,
                    "dayOfMonth": 4,
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-07-04",
                    "endDate": "2018-07-05",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUILA==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmcgAAAA==",
            "createdDateTime": "2017-07-30T08:44:46.8564224Z",
            "lastModifiedDateTime": "2017-07-30T08:44:46.8876729Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUILA==",
            "categories": [],
            "originalStartTimeZone": "China Standard Time",
            "originalEndTimeZone": "China Standard Time",
            "iCalUId": "040000008200E00074C5B7101A82E00800000000004111191009D301000000000000000010000000F301CC6F98E11B4AAA2BFC02FC3D7D54",
            "reminderMinutesBeforeStart": 15,
            "isReminderOn": true,
            "hasAttachments": false,
            "subject": "everyDay 8:00",
            "bodyPreview": "every day 9:00",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": false,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "oof",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmcgAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=us-ascii\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>every day 9:00</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-07-14T01:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-07-14T01:30:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "everyday Location"
            },
            "recurrence": {
                "pattern": {
                    "type": "daily",
                    "interval": 1,
                    "month": 0,
                    "dayOfMonth": 0,
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-07-14",
                    "endDate": "2017-10-06",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUIKw==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmcQAAAA==",
            "createdDateTime": "2017-07-30T08:43:44.3080899Z",
            "lastModifiedDateTime": "2017-07-30T08:43:47.3394114Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUIKw==",
            "categories": [],
            "originalStartTimeZone": "UTC",
            "originalEndTimeZone": "UTC",
            "iCalUId": "040000008200E00074C5B7101A82E00800000000C322C9F30F09D3010000000000000000100000000010C0112EF6CD4AAA57C7C2026944D7",
            "reminderMinutesBeforeStart": 30,
            "isReminderOn": true,
            "hasAttachments": false,
            "subject": "weekly",
            "bodyPreview": "HHH",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": true,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "busy",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmcQAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=us-ascii\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>HHH</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-07-29T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-07-30T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "weeklyLocation"
            },
            "recurrence": {
                "pattern": {
                    "type": "weekly",
                    "interval": 1,
                    "month": 0,
                    "dayOfMonth": 0,
                    "daysOfWeek": [
                        "saturday"
                    ],
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-07-29",
                    "endDate": "2018-01-13",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [
                {
                    "type": "optional",
                    "status": {
                        "response": "none",
                        "time": "0001-01-01T00:00:00Z"
                    },
                    "emailAddress": {
                        "name": "zhaoma@foxmail.com",
                        "address": "zhaoma@foxmail.com"
                    }
                },
                {
                    "type": "optional",
                    "status": {
                        "response": "none",
                        "time": "0001-01-01T00:00:00Z"
                    },
                    "emailAddress": {
                        "name": "zheng",
                        "address": "zhuzheng1977@bigpond.com"
                    }
                }
            ],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUIJw==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmcAAAAA==",
            "createdDateTime": "2017-07-30T07:32:19.2727864Z",
            "lastModifiedDateTime": "2017-07-30T07:32:19.2884125Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUIJw==",
            "categories": [],
            "originalStartTimeZone": "UTC",
            "originalEndTimeZone": "UTC",
            "iCalUId": "040000008200E00074C5B7101A82E0080000000037A3B4F90509D30100000000000000001000000049EF5A87BB582440A2AA6755D1C201E1",
            "reminderMinutesBeforeStart": 15,
            "isReminderOn": false,
            "hasAttachments": false,
            "subject": "开个会",
            "bodyPreview": "道道道",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": true,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "free",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmcAAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=gb2312\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>道道道</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-07-31T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-08-01T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "德州路"
            },
            "recurrence": {
                "pattern": {
                    "type": "weekly",
                    "interval": 1,
                    "month": 0,
                    "dayOfMonth": 0,
                    "daysOfWeek": [
                        "monday"
                    ],
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-07-31",
                    "endDate": "2018-01-15",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUIJg==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmbwAAAA==",
            "createdDateTime": "2017-07-30T07:29:20.221741Z",
            "lastModifiedDateTime": "2017-07-30T07:29:20.2686179Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUIJg==",
            "categories": [],
            "originalStartTimeZone": "China Standard Time",
            "originalEndTimeZone": "China Standard Time",
            "iCalUId": "040000008200E00074C5B7101A82E00800000000C29DFB8E0509D3010000000000000000100000006505190E3B35C54E944063DA4966C981",
            "reminderMinutesBeforeStart": 15,
            "isReminderOn": true,
            "hasAttachments": false,
            "subject": "XDM生日",
            "bodyPreview": "要不要喝一杯",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": false,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "busy",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmbwAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=iso-2022-jp\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>要不要喝一杯</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-07-27T01:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-07-27T01:30:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "黄河路"
            },
            "recurrence": {
                "pattern": {
                    "type": "absoluteYearly",
                    "interval": 1,
                    "month": 7,
                    "dayOfMonth": 27,
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-07-27",
                    "endDate": "2019-07-25",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUIJQ==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmbgAAAA==",
            "createdDateTime": "2017-07-30T07:28:26.3611113Z",
            "lastModifiedDateTime": "2017-07-30T07:28:26.376737Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUIJQ==",
            "categories": [],
            "originalStartTimeZone": "UTC",
            "originalEndTimeZone": "UTC",
            "iCalUId": "040000008200E00074C5B7101A82E00800000000E922E16E0509D301000000000000000010000000845880CF0B925848BB02AF422360EC9B",
            "reminderMinutesBeforeStart": 15,
            "isReminderOn": false,
            "hasAttachments": false,
            "subject": "响响生日",
            "bodyPreview": "吃火锅吧",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": true,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "free",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmbgAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=gb2312\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>吃火锅吧</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-10-29T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-10-30T00:00:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "西营路"
            },
            "recurrence": {
                "pattern": {
                    "type": "absoluteYearly",
                    "interval": 1,
                    "month": 10,
                    "dayOfMonth": 29,
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-10-29",
                    "endDate": "2019-10-27",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        },
        {
            "@odata.etag": "W/\"PvloAxD2xkOe+cBNAKR4TAABbRUIJA==\"",
            "id": "AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe_cBNAKR4TAAAAgENAAAAPvloAxD2xkOe_cBNAKR4TAABbMQmbQAAAA==",
            "createdDateTime": "2017-07-30T07:26:58.7184447Z",
            "lastModifiedDateTime": "2017-07-30T07:26:58.9997014Z",
            "changeKey": "PvloAxD2xkOe+cBNAKR4TAABbRUIJA==",
            "categories": [],
            "originalStartTimeZone": "China Standard Time",
            "originalEndTimeZone": "China Standard Time",
            "iCalUId": "040000008200E00074C5B7101A82E008000000009E4FA63A0509D301000000000000000010000000BE14A33A1F450E42862EFF9CC52F461A",
            "reminderMinutesBeforeStart": 15,
            "isReminderOn": true,
            "hasAttachments": false,
            "subject": "果果生日",
            "bodyPreview": "HAPPY BIRTHDAY~~",
            "importance": "normal",
            "sensitivity": "normal",
            "isAllDay": false,
            "isCancelled": false,
            "isOrganizer": true,
            "responseRequested": true,
            "seriesMasterId": null,
            "showAs": "busy",
            "type": "seriesMaster",
            "webLink": "https://outlook.live.com/owa/?itemid=AQMkADAwATE2ZTkwLWZkMDAALTJhOTUtMDACLTAwCgBGAAADScFE4vrqVUC7Tokm8bk9tgcAPvloAxD2xkOe%2BcBNAKR4TAAAAgENAAAAPvloAxD2xkOe%2BcBNAKR4TAABbMQmbQAAAA%3D%3D&exvsurl=1&path=/calendar/item",
            "onlineMeetingUrl": null,
            "responseStatus": {
                "response": "organizer",
                "time": "0001-01-01T00:00:00Z"
            },
            "body": {
                "contentType": "html",
                "content": "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n<meta content=\"text/html; charset=iso-2022-jp\">\r\n<style type=\"text/css\" style=\"display:none\">\r\n<!--\r\np\r\n\t{margin-top:0;\r\n\tmargin-bottom:0}\r\n-->\r\n</style>\r\n</head>\r\n<body dir=\"ltr\">\r\n<div id=\"divtagdefaultwrapper\" dir=\"ltr\" style=\"font-size:12pt; color:#000000; font-family:Calibri,Helvetica,sans-serif\">\r\n<p>HAPPY BIRTHDAY~~</p>\r\n</div>\r\n</body>\r\n</html>\r\n"
            },
            "start": {
                "dateTime": "2017-08-24T01:00:00.0000000",
                "timeZone": "UTC"
            },
            "end": {
                "dateTime": "2017-08-24T01:30:00.0000000",
                "timeZone": "UTC"
            },
            "location": {
                "displayName": "西营路",
                "address": {
                    "street": "西营路",
                    "city": "上海市",
                    "state": "上海市",
                    "countryOrRegion": "",
                    "postalCode": ""
                }
            },
            "recurrence": {
                "pattern": {
                    "type": "absoluteYearly",
                    "interval": 1,
                    "month": 8,
                    "dayOfMonth": 24,
                    "firstDayOfWeek": "sunday",
                    "index": "first"
                },
                "range": {
                    "type": "endDate",
                    "startDate": "2017-08-24",
                    "endDate": "2019-08-22",
                    "recurrenceTimeZone": "China Standard Time",
                    "numberOfOccurrences": 0
                }
            },
            "attendees": [],
            "organizer": {
                "emailAddress": {
                    "name": "karl Zhao",
                    "address": "zhaoma@hotmail.com"
                }
            }
        }
    ]
}
         */
    }
}
