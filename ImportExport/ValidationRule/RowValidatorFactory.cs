using Campus.DocumentValidator;

namespace SHSchool.Retake
{
    /// <summary>
    /// 用來產生排課系統所需的自訂驗證規則
    /// </summary>
    public class RowValidatorFactory : IRowValidatorFactory
    {
        #region IRowValidatorFactory 成員

        /// <summary>
        /// 根據typeName建立對應的RowValidator
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="validatorDescription"></param>
        /// <returns></returns>
        public IRowVaildator CreateRowValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "COURSENAMECHECK":
                    return new CourseNameCheck();
                case "RETAKESUBJECTLISTCHECK":
                    return new SubjectListCheck();
                default:
                    return null;
            }
        }

        #endregion
    }
}