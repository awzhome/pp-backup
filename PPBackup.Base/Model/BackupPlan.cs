﻿using System.Collections.ObjectModel;

namespace PPBackup.Base.Model
{
    public class BackupPlan : Model
    {
        public BackupPlan(string type) : base(type)
        {
        }

        private string? name;
        public string? Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChange();
            }
        }

        public ObservableCollection<BackupStep> Steps { get; } = new ObservableCollection<BackupStep>();
    }
}
