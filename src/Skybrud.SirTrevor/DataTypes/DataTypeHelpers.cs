using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.cms.businesslogic.datatype;

namespace Skybrud.SirTrevor.DataTypes {

    public class DataTypeHelpers {

        /// <summary>
        /// Deletes all pre valus for the specified data type.
        /// </summary>
        /// <param name="dataType"></param>
        public static void DeletePreValues(BaseDataType dataType) {
            SqlHelper.ExecuteNonQuery(
                "DELETE FROM cmsDataTypePreValues WHERE datatypenodeid = @dtdefid",
                SqlHelper.CreateParameter("@dtdefid", dataType.DataTypeDefinitionId)
            );
        }

        /// <summary>
        /// Saves the specified pre value for the data type.
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="alias"></param>
        /// <param name="value"></param>
        public static void SavePreValue(BaseDataType dataType, string alias, string value) {
            SqlHelper.ExecuteNonQuery(
                "INSERT INTO cmsDataTypePreValues (datatypenodeid,[value],sortorder,alias) VALUES (@dtdefid,@value,0,'" + alias + "')",
                SqlHelper.CreateParameter("@dtdefid", dataType.DataTypeDefinitionId),
                SqlHelper.CreateParameter("@value", value)
            );
        }

        /// <summary>
        /// Gets a pre value with the specified alias from the data type.
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static string GetPreValue(BaseDataType dataType, string alias) {
            var configuration = SqlHelper.ExecuteScalar<object>(
                "SELECT value FROM cmsDataTypePreValues WHERE datatypenodeid = @datatypenodeid AND alias = '" + alias + "'",
                SqlHelper.CreateParameter("@datatypenodeid", dataType.DataTypeDefinitionId)
            );
            return configuration != null ? configuration.ToString() : "";
        }

        public static ISqlHelper SqlHelper {
            get { return Application.SqlHelper; }
        }

    }
}