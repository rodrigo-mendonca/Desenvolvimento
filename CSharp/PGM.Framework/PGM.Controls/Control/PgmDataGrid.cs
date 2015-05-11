using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGM.Attributes;
using PGM.Common;
using PGM.Controls.PgmControl;

namespace PGM.Controls.PgmControl
{
    public partial class PgmDataGrid : UserControl
    {
        #region Propriedades
        ListSortDirection oOrderDir = ListSortDirection.Ascending; // Tipo da ordenação
        int oIndexOrderCol = 0; // Controle da coluna que está ordenada

        List<PgmHeader> oListHeader = new List<PgmHeader>(); // Lista de Layout das colunas
        ContextMenu oMenu; // Menu de Contexto

        /// <summary>
        /// Propriedade indica se as colunas podem ser ordenadas
        /// </summary>
        [Description("Propriedade indica se as colunas podem ser ordenadas")]
        public bool IsSortable { get; set; }

        #endregion

        #region Construtores
        public PgmDataGrid()
        {
            InitializeComponent();

            // Padrão
            IsSortable = true;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Adicionar Menu a grid
        /// </summary>
        /// <param name="tMenu">Menu já configurado</param>
        public void SetContextMenu(ContextMenu tMenu)
        {
            oMenu = tMenu;
        }

        /// <summary>
        /// Usando uma lista de Layout, configura todas as colunas
        /// </summary>
        /// <param name="oList">Lista da classe PGM.Controls.PgmHeader</param>
        public void SetHeader(List<PgmHeader> oList)
        {
            Grid.Columns.Clear();
            oListHeader = oList;

            foreach (PgmHeader oItem in oListHeader)
            {
                Grid.AutoGenerateColumns = false;
                DataGridViewColumn oCol = new DataGridViewColumn();

                switch (oItem.Control)
                {
                    case ControlType.TextBox:
                        oCol = new DataGridViewTextBoxColumn();
                        break;
                    case ControlType.CheckBox:
                        oCol = new DataGridViewCheckBoxColumn();
                        break;
                    case ControlType.ComboBox:
                        oCol = new DataGridViewComboBoxColumn();
                        break;
                    default:
                        break;
                }

                oCol.DataPropertyName = oItem.FieldName;
                oCol.HeaderText = oItem.ColumnName;
                oCol.Name = oItem.FieldName;
                oCol.DefaultCellStyle.NullValue = oItem.NullValue;
                oCol.DefaultCellStyle.Format = oItem.Format;
                oCol.SortMode = DataGridViewColumnSortMode.Programmatic;
                
                Grid.Columns.Add(oCol);
            }
        }

        /// <summary>
        /// Configuração padrão, caso não tenha passado um layout
        /// </summary>
        /// <param name="T">Classe passada para preencher a grade</param>
        private void SetStandardHeader(Type T)
        {
            foreach (DataGridViewColumn oItem in Grid.Columns)
            {
                Grid.AutoGenerateColumns = false;

                var oAttributes = T
                            .GetProperty(oItem.Name)
                            .GetCustomAttributes(typeof(GridHeader), false);

                if (oAttributes.Count() > 0)
                    oItem.HeaderText = (oAttributes.First() as GridHeader).Desc;

                oItem.DataPropertyName = oItem.Name;
                oItem.Name = oItem.Name;
                oItem.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        /// <summary>
        /// Exibe os dados da lista na grade
        /// </summary>
        /// <param name="tTable">Lista de uma classe T para preencher a grade</param>
        public void SetDataSource<T>(T tTable)
        {
            IsSortable = false;
            Grid.DataSource = tTable;

            // Exibe o numero de registros
            lblNumero.Text = Grid.RowCount.ToString();
        }

        /// <summary>
        /// Exibe os dados da lista na grade
        /// </summary>
        /// <param name="tTable">Lista de uma classe T para preencher a grade</param>
        public void SetDataSource<T>(List<T> tTable)
        {
            // a lista recebida é convertida para uma lista que pode ser ordenada
            SortableList<T> oSortList = new SortableList<T>((IEnumerable<T>)tTable);

            Grid.DataSource = oSortList;
            // Se não existir Header, usa os nomes configurados no modelo
            if (oListHeader.Count == 0)
            {
                SetStandardHeader(typeof(T));
            }

            // Exibe o numero de registros
            lblNumero.Text = Grid.RowCount.ToString();
            // Se existir colunas na grid, refaz a ordenação atual
            if (Grid.Columns.Count > 0)
                Grid.Sort(Grid.Columns[oIndexOrderCol], oOrderDir);
        }

        /// <summary>
        /// Define por qual coluna os dados devem ser ordenados
        /// </summary>
        /// <param name="tCol">Nome do campo na lista</param>
        public void SetColumnOrder(string tCol)
        {
            // Se a coluna existir, a define como a coluna de ordenação
            if (Grid.Columns[tCol].GetType() == typeof(DataGridViewColumn))
                oIndexOrderCol = Grid.Columns[tCol].Index;
        }

        /// <summary>
        /// Retorna uma DataGridViewRow da linha selecionada
        /// </summary>
        public DataGridViewRow GetSelectDataViewRow()
        {
            DataGridViewRow oRow = new DataGridViewRow();
            if (Grid.SelectedRows.Count != 0)
            {
                oRow = Grid.SelectedRows[0];
            }
            return oRow;     
        }

        /// <summary>
        /// Retorna o objeto do tipo selecionado
        /// </summary>
        public T GetSelectRow<T>()
        {
            T Retorno = Activator.CreateInstance<T>();
            int nIndex = 0;
            SortableList<T> oList;
            
            if (Grid.Rows.Count > 0)
            {
                nIndex = Grid.SelectedRows[0].Index;
                oList = (SortableList<T>)Grid.DataSource;
                Retorno = oList[nIndex];
            }

            return Retorno;
        }

        /// <summary>
        /// Retorna o PkId como Object
        /// </summary>
        /// <param name="tCol">Nome da coluna na lista</param>
        public object GetSelectRowColumn(string tCol)
        {
            IEnumerable IList = (IEnumerable) Grid.DataSource;
            Type elementType = IList.GetType().GetGenericArguments()[0];
            List<object> displayValues = IList.Cast<object>()
                                             .Select(v => elementType.GetProperty(tCol).GetValue(v, null))
                                             .ToList();

            return displayValues[Grid.SelectedRows[0].Index];
        }

        /// <summary>
        /// Limpar todos os registros da grade
        /// </summary>
        public void Clear()
        {
            Grid.Rows.Clear();
            // Exibe o numero de registros
            lblNumero.Text = Grid.RowCount.ToString();
        }
        #endregion

        #region Eventos
        public event EventHandler PgmDoubleClick;
        public virtual void DoPgmDoubleClick(object o, EventArgs e) { if (PgmDoubleClick != null)  PgmDoubleClick(o, e); }

        private void Grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DoPgmDoubleClick(sender, e);
        }

        private void Grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Grid.Rows.Count == 0)
                return;

            if (IsSortable)
            {
                ListSortDirection oDir;
                DataGridViewColumn oCol = Grid.Columns[e.ColumnIndex];
                DataGridViewColumn oSort = Grid.SortedColumn;
                if (oSort != null)
                {
                    if (oCol == oSort &&
                                Grid.SortOrder == SortOrder.Ascending)
                    {
                        oDir = ListSortDirection.Descending;
                    }
                    else
                    {
                        oDir = ListSortDirection.Ascending;
                        oCol.HeaderCell.SortGlyphDirection = SortOrder.None;
                    }
                }
                else
                    oDir = ListSortDirection.Ascending;

                Grid.Sort(oCol, oDir);
                oIndexOrderCol = e.ColumnIndex;
                oOrderDir = oDir;
            }
        }

        private void Grid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (IsSortable)
            {
                if (e.RowIndex < 0 && Grid.Rows.Count > 0)
                    Grid.Cursor = Cursors.PanSouth;
                else
                    Grid.Cursor = Cursors.Default;
            }
        }
        
        private void Grid_MouseClick(object sender, MouseEventArgs e)
        {
            if (oMenu != null)
            {
                int nRowIndex = Grid.HitTest(e.X, e.Y).RowIndex;
                if (e.Button == MouseButtons.Right && nRowIndex >= 0)
                    oMenu.Show(Grid, new Point(e.X, e.Y));
            }
        }

        private void PgmDataGrid_Resize(object sender, EventArgs e)
        {
            Grid.Width = this.Width;
            Grid.Height = this.Height - Grid.Top; ;
        }
        #endregion
    }
}