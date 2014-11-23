using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdPhone.Model.user
{

    public class userDetail
    {
        private userDetailInfo _user;

        public userDetailInfo user
        {
            get { return _user; }
            set { _user = value; }
        }

    }


    /// <summary>
    /// 用户详情
    /// </summary>
    public class userDetailInfo
    {

        private string _id;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _avatar;
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }


        private string _avatar100;
        /// <summary>
        /// 用户头像地址（大头像）
        /// </summary>
        public string avatar100
        {
            get { return _avatar100; }
            set { _avatar100 = value; }
        }

        private string _email;
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }


        private string _grade;
        /// <summary>
        /// 用户等级
        /// </summary>
        public string grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        private string _mark;
        /// <summary>
        /// 用户积分
        /// </summary>
        public string mark
        {
            get { return _mark; }
            set { _mark = value; }
        }

        private string _birth;
        /// <summary>
        /// 生日：yyyy-MM-dd
        /// </summary>
        public string birth
        {
            get { return _birth; }
            set { _birth = value; }
        }

        private int _gender;
        /// <summary>
        /// 性别：0表示未选；1表示男性；2表示女性
        /// </summary>
        public int gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _company;
        /// <summary>
        /// 公司
        /// </summary>
        public string company
        {
            get { return _company; }
            set { _company = value; }
        }


        private string _department;
        /// <summary>
        /// 所属部门
        /// </summary>
        public string department
        {
            get { return _department; }
            set { _department = value; }
        }

        private string _job;
        /// <summary>
        /// 职位
        /// </summary>
        public string job
        {
            get { return _job; }
            set { _job = value; }
        }


        private string _city;
        /// <summary>
        /// 居住地址
        /// </summary>
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }


        private string _job_number;
        /// <summary>
        /// 工号
        /// </summary>
        public string job_number
        {
            get { return _job_number; }
            set { _job_number = value; }
        }


        private string _work_site;
        /// <summary>
        /// 作业点
        /// </summary>
        public string Work_site
        {
            get { return _work_site; }
            set { _work_site = value; }
        }


        private string _mobile_phone;
        /// <summary>
        /// 移动电话
        /// </summary>
        public string mobile_phone
        {
            get { return _mobile_phone; }
            set { _mobile_phone = value; }
        }

        private string _work_phone;
        /// <summary>
        /// 工作电话
        /// </summary>
        public string work_phone
        {
            get { return _work_phone; }
            set { _work_phone = value; }
        }

        private string _create_time;
        /// <summary>
        /// 创建时间：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string create_time
        {
            get { return _create_time; }
            set { _create_time = value; }
        }


        private int _followed_status;
        /// <summary>
        /// 当前登录用户与该用户的关注关系：0表示未关注；1表示已关注
        /// </summary>
        public int followed_status
        {
            get { return _followed_status; }
            set { _followed_status = value; }
        }

        private int _egroup;
        /// <summary>
        /// 用户是否为外联群组用户：0为否；1为是
        /// </summary>
        public int egroup
        {
            get { return _egroup; }
            set { _egroup = value; }
        }

        private int _license;
        /// <summary>
        /// 当前用户的权限：-1表示普通用户；0表示既是管理员又是广播员；1表示管理员；2表示广播员
        /// </summary>
        public int license
        {
            get { return _license; }
            set { _license = value; }
        }

        private int _status;
        /// <summary>
        /// 用户状态：0为删除；1为正常；2为异常
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }

        private List<jobs> _jobs;

        public List<jobs> jobs
        {
            get { return _jobs; }
            set { _jobs = value; }
        }

        private List<educations> _educations;
        /// <summary>
        /// 
        /// </summary>
        public List<educations> educations
        {
            get { return _educations; }
            set { _educations = value; }
        }

        private project _project;
        /// <summary>
        /// 公司
        /// </summary>
        public project project
        {
            get { return _project; }
            set { _project = value; }
        }


    }


    public class jobs
    {
        private string _description;
        /// <summary>
        /// 工作内容
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _endDatel; 
        /// <summary>
        /// 结束年月
        /// </summary>
        public string endDatel
        {
            get { return _endDatel; }
            set { _endDatel = value; }
        }

        private string _name;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _startDate;
        /// <summary>
        /// 开始年月
        /// </summary>
        public string startDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _title;
        /// <summary>
        /// 职位
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }


    }


    public class educations {

        private string _description;
        /// <summary>
        /// 核心课程
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }


        private string _endDatel;
        /// <summary>
        /// 结束年月
        /// </summary>
        public string endDatel
        {
            get { return _endDatel; }
            set { _endDatel = value; }
        }

        private string _name;
        /// <summary>
        /// 学校名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _startDate;
        /// <summary>
        /// 开始年月
        /// </summary>
        public string startDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private string _title;
        /// <summary>
        /// 学位或学历
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }








    
    }


    public class project {

        private string _id;
        /// <summary>
        /// 企业ID
        /// </summary>
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        /// 企业中文名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _nameEn;
        /// <summary>
        /// 企业英文名称
        /// </summary>
        public string nameEn
        {
            get { return _nameEn; }
            set { _nameEn = value; }
        }

        private string _logo;
        /// <summary>
        /// 企业标识图片
        /// </summary>
        public string logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        private int _license_type;
        /// <summary>
        /// 网络的版本  0:免费版本；1：试用版本；2：高级模式版本
        /// </summary>
        public int license_type
        {
            get { return _license_type; }
            set { _license_type = value; }
        }
    
    }


}
