using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using umbraco.interfaces;

namespace Skybrud.SirTrevor.DataTypes {

    [ValidationProperty("IsValid")]
    public class DataEditor : UpdatePanel, IDataEditor {

        private readonly IData _data;
        protected readonly DataType DataType;
        protected TextBox ValueTextBox;

        public DataEditor(IData data, DataType dataType) {
            _data = data;
            DataType = dataType;
        }

        public virtual bool TreatAsRichTextEditor {
            get { return false; }
        }

        public bool ShowLabel {
            get { return true; }
        }

        public int DataPropertyId {
            get { return ((Data)_data).PropertyId; }
        }

        public Control Editor { get { return this; } }

        public string IsValid {
            get { return String.Empty; }
        }

        public void Save() {

            // Save the XML
            _data.Value = ValueTextBox.Text;

        }

        private void InitResources() {

            ClientDependencyHelpers.AddStyleSheet("/App_Plugins/SirTrevor/vendor/sir-trevor.css");
            ClientDependencyHelpers.AddStyleSheet("/App_Plugins/SirTrevor/vendor/sir-trevor-icons.css");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/vendor/underscore.js");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/vendor/eventable.js");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/vendor/sir-trevor.js");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/vendor/jquery.visible.js");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/vendor/jquery.scrollTo-1.4.3.1-min.js");

            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/before-blocks.js");
            ClientDependencyHelpers.AddJavascriptFolder("~/App_Plugins/SirTrevor/lib/");
            ClientDependencyHelpers.AddCssFolder("~/App_Plugins/SirTrevor/lib/");
            ClientDependencyHelpers.AddJavascriptFolder("~/App_Plugins/SirTrevor/blocks/");
            ClientDependencyHelpers.AddCssFolder("~/App_Plugins/SirTrevor/blocks/");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/blocks.js");
            ClientDependencyHelpers.AddJavaScript("/App_Plugins/SirTrevor/umbraco.js");

        }

        protected override void OnInit(EventArgs e) {

            base.OnInit(e);

            InitResources();

            HtmlGenericControl outerr = new HtmlGenericControl("div");
            outerr.Attributes.Add("class", "skybrudElements");
            outerr.Attributes.Add("style", "border: 1px solid black; width: 750px;");
            outerr.Attributes.Add("data-propertyid", DataPropertyId + "");
            ContentTemplateContainer.Controls.Add(outerr);

            ValueTextBox = new TextBox { TextMode = TextBoxMode.MultiLine };
            ValueTextBox.Attributes["class"] = "sir-trevor-editor sir-trevor-" + DataPropertyId;
            outerr.Controls.Add(ValueTextBox);

            //HtmlGenericControl script = new HtmlGenericControl("script");
            //script.EnableViewState = false;
            //script.Attributes["type"] = "text/javascript";
            //outerr.Controls.Add(script);

            //script.InnerHtml = "$(function(){\n";
            //script.InnerHtml += "  var editor = new SirTrevor.Editor({ el: $('.sir-trevor-" + DataPropertyId + "') });\n";
            //script.InnerHtml += "  editor.umbraco = {\n";
            //script.InnerHtml += "    dtdid: " + DataType.DataTypeDefinitionId + "\n";
            //script.InnerHtml += "  };\n";
            //script.InnerHtml += "  editor.onFormSubmit();\n";
            //script.InnerHtml += "  editor.$el.val( JSON.stringify(editor.dataStore, null, '  ') );\n";
            //script.InnerHtml += "});\n";


            string dataValue = _data.Value.ToString();
            if (dataValue.StartsWith("{") && dataValue.EndsWith("}")) {
                ValueTextBox.Text = dataValue;
            }



            HtmlGenericControl list = new HtmlGenericControl("div");
            list.Attributes.Add("class", "list");
            outerr.Controls.Add(list);

        }

    }

}