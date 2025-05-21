using AntdUI;

namespace MyExcelRibbon.Models
{
    internal class ExcelFile : NotifyProperty
    {
        private string name;
        private int index;
        private CellLink[] cellLinks;
        private CellProgress cellProgress;

        public string Id { get; set; }
        public string Path { get; set; }
        public string PDFPath { get; set; }

        public int Index
        {
            get { return index; }
            set
            {
                if (index == value) return;
                index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public CellProgress CellProgress
        {
            get { return cellProgress; }
            set
            {
                if (cellProgress == value) return;
                cellProgress = value;
                OnPropertyChanged(nameof(CellProgress));
            }
        }

        public CellLink[] CellLinks
        {
            get { return cellLinks; }
            set
            {
                if (cellLinks == value) return;
                cellLinks = value;
                OnPropertyChanged(nameof(CellLinks));
            }
        }
    }
}
