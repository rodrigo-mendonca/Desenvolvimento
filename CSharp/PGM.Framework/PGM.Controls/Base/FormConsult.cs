using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Controls.PgmControl;

namespace PGM.Controls
{
    public partial class FormConsult : FormBase
    {
        #region Contrutores
        public FormConsult()
        {
            InitializeComponent();
        }

        public PgmDataGrid GetGrid()
        {
            return Grid;
        }
        #endregion

        #region Metodos
        public void ConfigFrm<T>(List<T> tGrade)
        {
            Grid.SetDataSource(tGrade);
        }

        public void ConfigFrm<T>(List<T> tGrade,List<PgmHeader> tHeader)
        {
            Grid.SetHeader(tHeader);
            Grid.SetDataSource(tGrade);
        }

        public void ConfigFrm<T>(List<T> tGrade, ContextMenu tMenu)
        {
            Grid.SetContextMenu(tMenu);
            Grid.SetDataSource(tGrade);
        }

        public void ConfigFrm<T>(List<T> tGrade,List<PgmHeader> tHeader, ContextMenu tMenu)
        {
            Grid.SetContextMenu(tMenu);
            Grid.SetHeader(tHeader);
            Grid.SetDataSource(tGrade);
        }
        #endregion

        #region Eventos
        private void FormConsult_Resize(object sender, EventArgs e)
        {
            Grid.Width  = this.Width - 15;
            Grid.Height = this.Height - (Grid.Top+40);
        }

        public event EventHandler PgmAmbient;
        public virtual void DoPgmAmbient(object o, EventArgs e) { if (PgmAmbient != null)  PgmAmbient(o, e); }

        private void FormConsult_Load(object sender, EventArgs e)
        {
            DoPgmAmbient(sender, e);
        }
        #endregion

    }
}
