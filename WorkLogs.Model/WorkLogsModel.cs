using System;

namespace WorkLogs.Model
{
    
    public partial class WorkLogsModel
    {
        #region

        private int id;
        private DateTime dateTime;
        private string name;
        private String productName;
        private string version;
        private string stage;
        private string type;
        private string task;
        private string progress;
        private int whours;
        private string tprogress;
        private string workout;
        private string problem;

        public int ID { get => id; set => id = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public string Name { get => name; set => name = value; }
        public string ProductName { get => productName; set => productName = value; }
        public string Version { get => version; set => version = value; }
        public string Stage { get => stage; set => stage = value; }
        public string Type { get => type; set => type = value; }
        public string Task { get => task; set => task = value; }
        public string Progress { get => progress; set => progress = value; }
        public int Whours { get => whours; set => whours = value; }
        public string TProgress { get => tprogress; set => tprogress = value; }
        public string Workout { get => workout; set => workout = value; }
        public string Problem { get => problem; set => problem = value; }
        #endregion
    }

}
