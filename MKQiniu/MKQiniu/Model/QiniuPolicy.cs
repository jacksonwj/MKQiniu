namespace MKQiniu
{
    public class QiniuPolicy
    {
        /// <summary>
        /// [必需]bucket或者bucket:key
        /// </summary>
        public string Scope { get; set; }

        private int _deadline = 0;
        /// <summary>
        /// [必需]上传策略失效时刻
        /// </summary>
        public int Deadline
        {
            get { return _deadline; }
            set { _deadline = (int)Config.TIMESPAN.TotalSeconds + value; }
        }

        /// <summary>
        /// [可选]"仅新增"模式
        /// </summary>
        public int? InsertOnly { get; set; }

        /// <summary>
        /// [可选]保存文件的key
        /// </summary>
        public string SaveKey { get; set; }

        /// <summary>
        /// [可选]终端用户
        /// </summary>
        public string EndUser { get; set; }

        /// <summary>
        /// [可选]返回URL
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// [可选]返回内容
        /// </summary>
        public string ReturnBody { get; set; }

        /// <summary>
        /// [可选]回调URL
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// [可选]回调内容
        /// </summary>
        public string CallbackBody { get; set; }

        /// <summary>
        /// [可选]回调内容类型
        /// </summary>
        public string CallbackBodyType { get; set; }

        /// <summary>
        /// [可选]回调host
        /// </summary>
        public string CallbackHost { get; set; }

        /// <summary>
        /// [可选]回调fetchkey
        /// </summary>
        public int? CallbackFetchKey { get; set; }

        /// <summary>
        /// [可选]上传预转持久化
        /// </summary>
        public string PersistentOps { get; set; }

        /// <summary>
        /// [可选]持久化结果通知
        /// </summary>
        public string PersistentNotifyUrl { get; set; }

        /// <summary>
        /// [可选]私有队列
        /// </summary>
        public string PersistentPipeline { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最小值
        /// </summary>
        public int? FileSizeMin { get; set; }

        /// <summary>
        /// [可选]上传文件大小限制：最大值
        /// </summary>
        public int? FileSizeLimit { get; set; }

        /// <summary>
        /// [可选]上传时是否自动检测MIME
        /// </summary>
        public int? DetectMime { get; set; }

        /// <summary>
        /// [可选]上传文件MIME限制
        /// </summary>
        public string MimeLimit { get; set; }

        /// <summary>
        /// [可选]文件上传后多少天后自动删除
        /// </summary>
        public int? DeleteAfterDays { get; set; }

        //internal string ToJsonString()
        //{
        //    var result = new StringBuilder("{ ");

        //    result.AppendFormat("\"scope\": \"{0}\"", Scope); //必需
        //    result.AppendFormat(", \"deadline\": {0}", Deadline);  //必需

        //    if (InsertOnly.HasValue)
        //    {
        //        result.AppendFormat(", \"inserOnly\": {0}", InsertOnly);
        //    }

        //    if (!string.IsNullOrWhiteSpace(SaveKey))
        //    {
        //        result.AppendFormat(", \"saveKey\": \"{0}\"", SaveKey);
        //    }

        //    if (!string.IsNullOrWhiteSpace(EndUser))
        //    {
        //        result.AppendFormat(", \"endUser\": \"{0}\"", EndUser);
        //    }

        //    if (!string.IsNullOrWhiteSpace(ReturnUrl))
        //    {
        //        result.AppendFormat(", \"returnUrl\": \"{0}\"", ReturnUrl);
        //    }

        //    if (!string.IsNullOrWhiteSpace(ReturnBody))
        //    {
        //        result.AppendFormat(", \"returnBody\": \"{0}\"", ReturnBody);
        //    }

        //    if (!string.IsNullOrWhiteSpace(CallbackUrl))
        //    {
        //        result.AppendFormat(", \"callbackUrl\": \"{0}\"", CallbackUrl);
        //    }

        //    if (!string.IsNullOrWhiteSpace(CallbackBody))
        //    {
        //        result.AppendFormat(", \"callbackBody\": \"{0}\"", CallbackBody);
        //    }

        //    if (!string.IsNullOrWhiteSpace(CallbackBodyType))
        //    {
        //        result.AppendFormat(", \"callbackBodyType\": \"{0}\"", CallbackBodyType);
        //    }

        //    if (!string.IsNullOrWhiteSpace(CallbackHost))
        //    {
        //        result.AppendFormat(", \"calbackHost\": \"{0}\"", CallbackHost);
        //    }

        //    if (CallbackFetchKey.HasValue)
        //    {
        //        result.AppendFormat(", \"callbackFetchKey\": {0}", CallbackFetchKey);
        //    }

        //    if (!string.IsNullOrWhiteSpace(PersistentOps))
        //    {
        //        result.AppendFormat(", \"persistentOps\": \"{0}\"", PersistentOps);
        //    }

        //    if (!string.IsNullOrWhiteSpace(PersistentNotifyUrl))
        //    {
        //        result.AppendFormat(", \"persistentNotifyUrl\": \"{0}\"", PersistentNotifyUrl);
        //    }

        //    if (!string.IsNullOrWhiteSpace(PersistentPipeline))
        //    {
        //        result.AppendFormat(", \"persistentPipeline\": \"{0}\"", PersistentPipeline);
        //    }

        //    if (FileSizeMin.HasValue)
        //    {
        //        result.AppendFormat(", \"fsizeMin\": {0}", FileSizeMin);
        //    }

        //    if (FileSizeLimit.HasValue)
        //    {
        //        result.AppendFormat(", \"fsizeLimit\": {0}", FileSizeLimit);
        //    }

        //    if (DetectMime.HasValue)
        //    {
        //        result.AppendFormat(", \"detectMime\": {0}", DetectMime);
        //    }

        //    if (!string.IsNullOrWhiteSpace(MimeLimit))
        //    {
        //        result.AppendFormat(", \"mimeLimit\": \"{0}\"", MimeLimit);
        //    }

        //    if (DeleteAfterDays.HasValue)
        //    {
        //        result.AppendFormat(", \"deleteAfterDays\": {0}", DeleteAfterDays);
        //    }

        //    result.Append(" }");
        //    return result.ToString();
        //}
    }
}
