using System;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Skybrud.SirTrevor.DataTypes {
    
    public class SirTrevorDataType : BaseDataType, IDataType {
        
        private IDataEditor _dataEditor;
        private IData _baseData;
        private SirTrevorPrevalueEditor _prevalueEditor;

        public override IDataEditor DataEditor {
            get {
                return _dataEditor ?? (_dataEditor = new SirTrevorDataEditor(Data, this));
            }
        }

        public override IDataPrevalue PrevalueEditor {
            get { return _prevalueEditor ?? (_prevalueEditor = new SirTrevorPrevalueEditor(this)); }
        }

        public override IData Data {
            get { return _baseData ?? (_baseData = new SirTrevorData(this)); }
        }

        public override Guid Id {
            get { return new Guid("2ef0b050-0d70-4605-8549-6267f6992e29"); }
        }

        public override string DataTypeName {
            get { return "Skybrud - Sir Trevor"; }
        }

    }

}
