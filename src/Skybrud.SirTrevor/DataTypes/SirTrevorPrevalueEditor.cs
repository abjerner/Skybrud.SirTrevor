using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Skybrud.SirTrevor.Blocks;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Skybrud.SirTrevor.DataTypes {
    public class SirTrevorPrevalueEditor : PlaceHolder, IDataPrevalue {

        private class CheckBoxTypePair {
            public CheckBox CheckBox { get; set; }
            public SirTrevorBlockType BlockType { get; set; }
        }

        private List<CheckBoxTypePair> _chkBlocks = new List<CheckBoxTypePair>();
        HtmlTable _blockTypesTable = new HtmlTable();
        TextBox _txtTwitterClientId = new TextBox();
        TextBox _txtTwitterClientSecret = new TextBox();

        protected readonly umbraco.cms.businesslogic.datatype.BaseDataType DataType;

        public Control Editor {
            get { return this; }
        }

        public SirTrevorPrevalueEditor(BaseDataType dataType) {
            
            DataType = dataType;

            SetupChildControls();
        
        }

        private void SetupChildControls() {

            foreach (SirTrevorBlockType blockType in SirTrevorBlockType.GetBlockTypes()) {

                HtmlTableRow row = new HtmlTableRow();

                HtmlTableCell c1 = new HtmlTableCell();
                row.Cells.Add(c1);

                HtmlTableCell c2 = new HtmlTableCell();
                row.Cells.Add(c2);

                CheckBox checkBox = new CheckBox();
                _chkBlocks.Add(new CheckBoxTypePair {
                    CheckBox = checkBox,
                    BlockType = blockType
                });
                c1.Controls.Add(checkBox);

                c2.Controls.Add(new Literal { Text = blockType.Name });

                _blockTypesTable.Rows.Add(row);

            }

            Controls.Add(_blockTypesTable);

            #region Twitter options
            
            _txtTwitterClientId.Attributes["size"] = "42";
            _txtTwitterClientSecret.Attributes["size"] = "42";
            Controls.Add(_txtTwitterClientId);
            Controls.Add(_txtTwitterClientSecret);

            #endregion

        }

        protected override void OnLoad(EventArgs e) {

            base.OnLoad(e);

            if (Page.IsPostBack) {

                // do nothing here yet

            } else {

                string[] allowedBlocks = (DataTypeHelpers.GetPreValue(DataType, "AllowedBlocks") ?? "").Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var bacon in _chkBlocks) {
                    if (allowedBlocks.Contains(bacon.BlockType.Id)) {
                        bacon.CheckBox.Checked = true;
                    }
                }

                _txtTwitterClientId.Text = DataTypeHelpers.GetPreValue(DataType, "TwitterClientId");
                _txtTwitterClientSecret.Text = DataTypeHelpers.GetPreValue(DataType, "TwitterClientSecret");

            }

        }

        public void Save() {

            // Delete any existing pre-values (is this cool?)
            DataTypeHelpers.DeletePreValues(DataType);

            // Save a comma separated list of blocks that should be allowed
            DataTypeHelpers.SavePreValue(DataType, "AllowedBlocks", String.Join(",", _chkBlocks.Where(x => x.CheckBox.Checked).Select(x => x.BlockType.Id)));

            // Save the Twitter OAuth information
            DataTypeHelpers.SavePreValue(DataType, "TwitterClientId", _txtTwitterClientId.Text.Trim());
            DataTypeHelpers.SavePreValue(DataType, "TwitterClientSecret", _txtTwitterClientSecret.Text.Trim());

        }

        protected override void Render(HtmlTextWriter writer) {

            writer.WriteLine("<div class=\"propertyItem\">\n");

            writer.WriteLine("<strong>Blocks</strong><br />\n");
            _blockTypesTable.RenderControl(writer);

            writer.WriteLine("<br />\n");
            writer.WriteLine("<strong>Tweet</strong><br />\n");
            writer.WriteLine("<span style=\"font-size: 11px;\">Lets editors insert a block with a tweet (status message) by entering or pasting the URL of the tweet. This feature requires access to the Twitter API, which again requires you to enter information about your app in the Twitter API. <a href=\"#\">Read more here</a></span><br />\n");

            writer.WriteLine("<table>\n");
            writer.WriteLine("<tr>\n");
            writer.WriteLine("<td><strong>Client ID</strong></td>\n");
            writer.WriteLine("<td>\n");
            _txtTwitterClientId.RenderControl(writer);
            writer.WriteLine("</td>\n");
            writer.WriteLine("</tr>\n");

            writer.WriteLine("<tr>\n");
            writer.WriteLine("<td><strong>Client Secret</strong></td>\n");
            writer.WriteLine("<td>\n");
            _txtTwitterClientSecret.RenderControl(writer);
            writer.WriteLine("</td>\n");
            writer.WriteLine("</tr>\n");

            writer.WriteLine("</table>\n");

            writer.WriteLine("</div>\n");
        
        }

    }

}
