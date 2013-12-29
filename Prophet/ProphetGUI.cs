using System;
using System.Windows.Forms;

namespace Prophet {
    public partial class ProphetGUI : Form {
        public ProphetGUI() {
            InitializeComponent();
        }

        private void ProphetGUI_Load(object sender, EventArgs e) {
            prophetPropertyGrid.SelectedObject = PartySettings.Instance;
        }

        private void prophetPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            var o = prophetPropertyGrid.SelectedObject as PartySettings;

            if(o != null) {
                o.Save();
            }
        }
    }
}
