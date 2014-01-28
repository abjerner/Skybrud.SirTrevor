using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using umbraco.interfaces;
using System.Linq;

namespace Skybrud.SirTrevor.DataTypes {

    [ValidationProperty("IsValid")]
    public class SirTrevorDataEditor : UpdatePanel, IDataEditor {
        
        private readonly IData _data;
        protected readonly SirTrevorDataType DataType;
        protected TextBox ValueTextBox;

        public SirTrevorDataEditor(IData data, SirTrevorDataType dataType) {
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
            get { return ((SirTrevorData) _data).PropertyId; }
        }

        public Control Editor { get { return this; } }

        public string IsValid {
            get  { return String.Empty; }
        }

        public void Save() {

            // Save the XML
            _data.Value = ValueTextBox.Text;
        
        }

        protected override void OnInit(EventArgs e) {

            base.OnInit(e);

            AddStyleSheet(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/sir-trevor.css"));
            AddStyleSheet(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/sir-trevor-icons.css"));

            AddJavaScript(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/underscore.js"));
            AddJavaScript(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/eventable.js"));
            AddJavaScript(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/sir-trevor.js"));
            //AddJavaScript(GetCachableUrl("/Umbraco/Skybrud/SirTrevor/umbraco.js"));
            
            HtmlGenericControl outerr = new HtmlGenericControl("div");
            outerr.Attributes.Add("class", "skybrudElements");
            outerr.Attributes.Add("style", "border: 1px solid black; width: 750px;");
            outerr.Attributes.Add("data-propertyid", DataPropertyId + "");
            ContentTemplateContainer.Controls.Add(outerr);

            ValueTextBox = new TextBox { TextMode = TextBoxMode.MultiLine };
            ValueTextBox.Attributes["class"] = "sir-trevor-" + DataPropertyId;
            outerr.Controls.Add(ValueTextBox);

            HtmlGenericControl script = new HtmlGenericControl("script");
            script.EnableViewState = false;
            script.Attributes["type"] = "text/javascript";
            outerr.Controls.Add(script);

            script.InnerHtml = "$(function(){\n";
            script.InnerHtml += "  var editor = new SirTrevor.Editor({ el: $('.sir-trevor-" + DataPropertyId + "') });\n";
            script.InnerHtml += "  editor.umbraco = {\n";
            script.InnerHtml += "    dtdid: " + DataType.DataTypeDefinitionId + "\n";
            script.InnerHtml += "  };\n";
            script.InnerHtml += "  editor.onFormSubmit();\n";
            script.InnerHtml += "  editor.$el.val( JSON.stringify(editor.dataStore, null, '  ') );\n";
            script.InnerHtml += "});\n";



            string dataValue = _data.Value.ToString();
            if (dataValue.StartsWith("{") && dataValue.EndsWith("}")) {
                ValueTextBox.Text = dataValue;
            }



            HtmlGenericControl list = new HtmlGenericControl("div");
            list.Attributes.Add("class", "list");
            outerr.Controls.Add(list);

        }

        private string GetCachableUrl(string url) {
            if (url.StartsWith("/")) {
                string path = Page.Server.MapPath(url);
                return File.Exists(path) ? url + "?" + File.GetLastWriteTime(path).Ticks : url;
            }
            return url;
        }

        private void AddJavaScript(string url) {

            if (Page.Header.Controls.OfType<HtmlControl>().Any(control => control.TagName == "script" && control.Attributes["src"] == url)) {
                return;
            }

            HtmlGenericControl script = new HtmlGenericControl("script");
            script.Attributes["type"] = "text/javascript";
            script.Attributes["src"] = url;
            Page.Header.Controls.Add(script);

        }

        private void AddStyleSheet(string url) {

            if (Page.Header.Controls.OfType<HtmlControl>().Any(control => control.TagName == "link" && control.Attributes["type"] == "text/css" && control.Attributes["href"] == url)) {
                return;
            }

            HtmlLink link = new HtmlLink { Href = url };
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(link);

        }

    }

}