namespace net_core_bootcamp_b1.Helpers
{
    /// <summary>
    ///  5 Karakter olacak
    ///  Ilk 2 karakter ekranı belirtecek.
    ///  3. Karakter mesaj türünü belirtecek. (I : info, W:Warning, E:Error )
    ///  Son 2 karakter hatayı belirtecek.
    /// </summary>
    public class ApiResultMessages
    {
        /// <summary>
        /// All Process Succesfull
        /// </summary>
        public const string Ok = "Ok";
        
        #region Product

        /// <summary>
        /// No Products Found.
        /// </summary>
        public const string PRE01 = "PRE01";

        #endregion

        #region ProductCategory

        /// <summary>
        /// No Product Category Found.
        /// </summary>
        public const string PCE01 = "PCE01";

        #endregion
    }
}
