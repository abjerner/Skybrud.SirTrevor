using System;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Skybrud.SirTrevor.DataTypes {
    
    public class DataType : BaseDataType, IDataType {
        
        private IDataEditor _dataEditor;
        private IData _baseData;
        private PrevalueEditor _prevalueEditor;

        public override IDataEditor DataEditor {
            get {
                return _dataEditor ?? (_dataEditor = new DataEditor(Data, this));
            }
        }

        public override IDataPrevalue PrevalueEditor {
            get { return _prevalueEditor ?? (_prevalueEditor = new PrevalueEditor(this)); }
        }

        public override IData Data {
            get { return _baseData ?? (_baseData = new Data(this)); }
        }

        public override Guid Id {
            get { return new Guid("2ef0b050-0d70-4605-8549-6267f6992e29"); }
        }

        public override string DataTypeName {
            get { return "Skybrud - Sir Trevor"; }
        }

    }

}
