// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class SignatureRequestWrapper
    {
        [JsonProperty("signature_request")]
        public SignatureRequest SignatureRequest { get; set; }
    }

    public class SignatureRequest
    {
        [JsonProperty("test_mode")]
        public bool TestMode { get; internal set; }
        [JsonProperty("signature_request_id")]
        public string SignatureRequestId { get; internal set; }
        [JsonProperty("requester_email_address")]
        public string RequesterEmailAddress { get; internal set; }
        [JsonProperty("title")]
        public string Title { get; internal set; }
        [JsonProperty("subject")]
        public string Subject { get; internal set; }
        [JsonProperty("message")]
        public string Message { get; internal set; }
        [JsonProperty("is_complete")]
        public bool IsComplete { get; internal set; }
        [JsonProperty("has_error")]
        public bool HasError { get; internal set; }

        [Obsolete]
        [JsonProperty("final_copy_uri")]
        public string FinalCopyUri { get; internal set; }

        [JsonProperty("files_url")]
        public string FilesUrl { get; internal set; }        
        
        [JsonProperty("signing_url")]
        public string SigningUrl { get; internal set; }
        [JsonProperty("signing_redirect_url")]
        public string SigningRedirectUrl { get; internal set; }
        [JsonProperty("details_url")]
        public string DetailsUrl { get; internal set; }
        [JsonProperty("cc_email_addresses")]
        public IList<string> CcEmailAddresses { get; internal set; }
        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; internal set; }
        [JsonProperty("response_data")]
        public IList<ResponseData> ResponseData { get; internal set; }
        [JsonProperty("signatures")]
        public IList<Signature> Signatures { get; internal set; }
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }
    }

    public class ResponseData
    {
        [JsonProperty("api_id")]
        public string ApiId { get; internal set; }
        [JsonProperty("signature_id")]
        public string SignatureId { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("value")]
        public object Value { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
    }

    public class Signature
    {
        [JsonProperty("signature_id")]
        public string SignatureId { get; internal set; }
        [JsonProperty("signer_email_address")]
        public string SignerEmailAddress { get; internal set; }
        [JsonProperty("signer_name")]
        public string SignerName { get; internal set; }
        [JsonProperty("order")]
        public int? Order { get; internal set; }
        [JsonProperty("status_code")]
        public string StatusCode { get; internal set; }
        [JsonProperty("signed_at")]
        public long? SignedAt { get; internal set; }
        [JsonProperty("last_viewed_at")]
        public long? LastViewedAt { get; internal set; }
        [JsonProperty("last_reminded_at")]
        public long? LastRemindedAt { get; internal set; }
        [JsonProperty("has_pin")]
        public bool HasPin { get; internal set; }
    }

    public class SignatureRequestList
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; internal set; }
        [JsonProperty("signature_requests")]
        public IList<SignatureRequest> SignatureRequests { get; internal set; }
    }

    public class SignatureRequestGetRequest : HelloSignRequestBase
    {
        /// <summary>
        /// The id of the SignatureRequest to retrieve.
        /// </summary>
        [JsonProperty("signature_request_id")]
        public string SignatureRequestId { get; set; }
    }

    public abstract class HelloSignRequestBase : IHelloSignRequest
    {
        public virtual IDictionary<string, object> ToRequestParams()
        {
            return JObject.FromObject(this).ToObject<Dictionary<string, object>>();
        }
    }

    public class SignatureRequestListRequest : IHelloSignRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string AccountId { get; set; }
        public string Query { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.AddOptionalField("page", Page);
            data.AddOptionalField("page_size", PageSize);
            data.AddOptionalField("account_Id", AccountId);
            data.AddOptionalField("query", Query);

            return data;
        }
    }

    public class SignatureRequestSendRequest : IHelloSignRequestWithFiles
    {
        public SignatureRequestSendRequest()
        {
            Files = new MultiFileRequest();
        }

        /// <summary>
        /// Whether this is a test, the signature request will not be legally binding if set to 1. Defaults to 0.
        /// </summary>
        public int? TestMode { get; set; }
        /// <summary>
        /// Use file[] to indicate the uploaded file(s) to send for signature. 
        /// Currently we only support use of either the file[] parameter or file_url[] parameter, not both.
        /// </summary>
        public MultiFileRequest Files { get; set; }
        /// <summary>
        /// Use file_url[] to have HelloSign download the file(s) to send for signature. 
        /// Currently we only support use of either the file[] parameter or file_url[] parameter, not both.
        /// </summary>
        public IList<string> FileUrls { get; set; }
        /// <summary>
        /// The title you want to assign to the SignatureRequest.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The subject in the email that will be sent to the signers.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The custom message in the email that will be sent to the signers.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The URL you want the signer redirected to after they successfully sign.
        /// </summary>
        public string SigningRedirectUrl { get; set; }
        /// <summary>
        /// The client ID of the ApiApp you want to associate with this request.
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// [boolean] Send with a value of 1 if you wish to enable Text Tags parsing in your document. Defaults to 0.
        /// </summary>
        public int? UseTextTags { get; set; }
        /// <summary>
        /// [boolean] Send with a value of 1 if you wish to enable automatic Text Tag removal. Defaults to 0. When using Text Tags it is preferred that you set this to 0 and hide your tags with white text or something similar because the automatic removal system can cause unwanted clipping. See the Text Tags walkthrough for more details.
        /// </summary>
        public int? HideTextTags { get; set; }
        public IList<SignerRequest> Signers { get; set; }
        /// <summary>
        /// The email addresses that should be CCed.
        /// </summary>
        public IList<string> CcEmailAddresses { get; set; }
        /// <summary>
        /// Key-value data that should be attached to the signature request. This metadata is included in all 
        /// API responses and events involving the signature request. For example, use the metadata 
        /// field to store a signer's order number for look up when receiving events for the signature request.
        /// </summary>
        /// <remarks>
        /// Each request can include up to 10 metadata keys, with key names up to 40 characters long and 
        /// values up to 500 characters long.
        /// </remarks>
        public IDictionary<string, string> Metadata { get; set; }
        /// <summary>
        /// The fields that should appear on the document, expressed as a 2-dimensional JSON array 
        /// serialized to a string. The main array represents documents, with each containing an array 
        /// of form fields. One document array is required for each file provided by the file[] parameter. 
        /// In the case of a file with no fields, an empty list must be specified.
        /// </summary>
        public IList<IList<FormFieldsRequest>> FormFieldsPerDocument { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.AddOptionalField("test_mode", TestMode);
            data.AddOptionalListField("file_urls", FileUrls);
            data.AddOptionalField("title", Title);
            data.AddOptionalField("subject", Subject);
            data.AddOptionalField("message", Message);
            data.AddOptionalField("signing_redirect_url", SigningRedirectUrl);
            data.AddOptionalField("client_id", ClientId);
            data.AddOptionalListField("cc_email_addresses", CcEmailAddresses);
            data.AddOptionalField("use_text_tags", UseTextTags);
            data.AddOptionalField("hide_text_tags", HideTextTags);
            data.AddOptionalDictionaryField("metadata", Metadata);

            for (int i = 0; i < Signers.Count; i++)
            {
                data.Add("signers[" + i + "][name]", Signers[i].Name);
                data.Add("signers[" + i + "][email_address]", Signers[i].EmailAddress);
                data.AddOptionalField("signers[" + i + "][order]", Signers[i].Order);
                data.AddOptionalField("signers[" + i + "][pin]", Signers[i].Pin);
            }

            data.AddOptionalJsonField("form_fields_per_document", FormFieldsPerDocument);

            return data;
        }

        public IEnumerable<FileRequest> GetFileRequests()
        {
            return (null != Files) ? Files.GetRequests() : Enumerable.Empty<FileRequest>();
        }

        public IEnumerable<KeyValuePair<string, FileRequest>> GetFileRequestEntries()
        {
            return (null != Files) ?
                Files.GetRequests()
                    .Select((fs, i) => Tuple.Create(fs, i))
                    .ToDictionary(fse => "file[" + fse.Item2 + "]", fse => fse.Item1) :
                Enumerable.Empty<KeyValuePair<string, FileRequest>>();
        }
    }

    public class MultiFileRequest
    {
        private List<FileRequest> requests = new List<FileRequest>();

        public MultiFileRequest AddFile(string localFilePath)
        {
            return AddFile(Path.GetFileName(localFilePath), () => new FileStream(localFilePath, FileMode.Open));
        }

        public MultiFileRequest AddFiles(params string[] localFilePaths)
        {
            foreach (var path in localFilePaths)
            {
                AddFile(Path.GetFileName(path), () => new FileStream(path, FileMode.Open));
            }

            return this;
        }

        public MultiFileRequest AddFile(string fileName, Func<Stream> getStream)
        {
            return AddFile(fileName, () => Task.FromResult(getStream()));
        }

        public MultiFileRequest AddFile(string fileName, Func<Task<Stream>> getStreamAsync)
        {
            requests.Add(FileRequest.Create(fileName, getStreamAsync));
            return this;
        }

        internal IEnumerable<FileRequest> GetRequests()
        {
            return requests;
        }
    }

    public class FileRequest
    {
        private Func<Task<Stream>> getStreamAsync;

        private FileRequest(string fileName, Func<Task<Stream>> getStreamAsync)
        {
            FileName = fileName;
            this.getStreamAsync = getStreamAsync;
        }

        public static FileRequest Create(string fileName, Func<Task<Stream>> getStreamAsync)
        {
            return new FileRequest(fileName, getStreamAsync);
        }

        public string FileName { get; set; }

        protected internal Task<Stream> GetStreamAsync()
        {
            return getStreamAsync();
        }
    }

    public static class RequestDataDictionaryExtensions
    {
        public static void AddOptionalField(this IDictionary<string, object> data, string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
                return;

            data.Add(fieldName, fieldValue);
        }

        public static void AddOptionalField<T>(this IDictionary<string, object> data, string fieldName, Nullable<T> fieldValue)
            where T : struct
        {
            if (fieldValue == null)
                return;

            data.Add(fieldName, fieldValue.Value);
        }

        public static void AddOptionalListField<T>(this IDictionary<string, object> data, string fieldName, IList<T> listField)
        {
            if (listField == null)
                return;

            for (var i = 0; i < listField.Count; i++)
            {
                data.Add(string.Format("{0}[", fieldName) + i + "]", listField[i]);
            }
        }

        public static void AddOptionalJsonField<T>(this IDictionary<string, object> data, string fieldName, IEnumerable<T> jsonListField)
        {
            if (jsonListField == null || jsonListField.Count() == 0)
                return;

            data.Add(fieldName, JsonConvert.SerializeObject(jsonListField));
        }

        public static void AddOptionalDictionaryField<T>(this IDictionary<string, object> data, string fieldName, IDictionary<string, T> dictionaryField)
        {
            if (dictionaryField == null)
                return;

            foreach (var entry in dictionaryField)
            {
                data.Add(string.Format("{0}[", fieldName) + entry.Key + "]", entry.Value);
            }
        }
    }

    public class SignerRequest
    {
        /// <summary>
        /// The name of the signer.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The email address of the signer.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The order the signer is required to sign in.
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// The 4- to 12-character access code that will secure this signer's signature page.
        /// </summary>
        public string Pin { get; set; }
    }

    public class FormFieldsRequest
    {
        /// <summary>
        /// an identifier for the field that is unique across all documents in the request 
        /// </summary>
        [JsonProperty("api_id")]
        public string ApiId { get; set; }
        /// <summary>
        /// This is a display name for the field.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// one of the Type options 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// location coordinates of the field in pixels 
        /// </summary>
        [JsonProperty("x")]
        public int X { get; set; }
        /// <summary>
        /// location coordinates of the field in pixels 
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; set; }
        /// <summary>
        /// size of the field in pixels 
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// whether this field is required
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }
        /// <summary>
        /// signer index identified by the offset %i% in the signers[%i%] parameter, indicating 
        /// which signer should fill out the field 
        /// </summary>
        [JsonProperty("signer")]
        public int SignerIndex { get; set; }
        /// <summary>
        /// Each text field may contain a validation_type parameter.
        /// Check out the list of validation types to learn more about the possible values. 
        /// </summary>
        [JsonProperty("validation_type")]
        public string ValidationType { get; set; }
    }

    public class SignatureRequestSendReusableFormRequest : IHelloSignRequest
    {
        public int? TestMode { get; set; }
        public string ReusableFormId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IDictionary<string, SignatureRequestSignerRoleRequest> Signers { get; set; }
        public IDictionary<string, string> Ccs { get; set; }
        public IDictionary<string, string> CustomFields { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            if (TestMode.HasValue)
                data.Add("test_mode", TestMode.Value);

            data.Add("reusable_form_id", ReusableFormId);

            if (!string.IsNullOrEmpty(Title))
                data.Add("title", Title);

            if (!string.IsNullOrEmpty(Subject))
                data.Add("subject", Subject);

            if (!string.IsNullOrEmpty(Message))
                data.Add("message", Message);

            foreach (KeyValuePair<string, SignatureRequestSignerRoleRequest> signer in Signers)
            {
                data.Add("signers[" + signer.Key + "][name]", signer.Value.Name);
                data.Add("signers[" + signer.Key + "][email_address]", signer.Value.EmailAddress);
            }

            if (Ccs != null)
            {
                foreach (KeyValuePair<string, string> cc in Ccs)
                {
                    data.Add("ccs[" + cc.Key + "][email_address]", cc.Value);
                }
            }

            if (CustomFields != null)
            {
                foreach (KeyValuePair<string, string> customField in CustomFields)
                {
                    data.Add("custom_fields[" + customField.Key + "]", customField.Value);
                }
            }

            return data;
        }
    }

    public class SignatureRequestFromTemplateRequest : IHelloSignRequest
    {
        /// <summary>
        /// Whether this is a test, the signature request will 
        /// not be legally binding if set to 1. Defaults to 0.
        /// </summary>
        public int? TestMode { get; set; }
        /// <summary>
        /// Use template_id to create a SignatureRequest from a single Template. 
        /// Use template_ids[%i%] to create a SignatureRequest from multiple templates, 
        /// where %i% is an integer indicating the order in which the template will be used. 
        /// Only template_id or template_ids[%i%] can be used, not both.
        /// </summary>
        public IList<string> TemplateIds { get; set; }
        /// <summary>
        /// The title you want to assign to the SignatureRequest.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The subject in the email that will be sent to the signers.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// The custom message in the email that will be sent to the signers.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The URL you want the signer redirected to after they successfully sign.
        /// </summary>
        public string SigningRedirectUrl { get; set; }
        /// <summary>
        /// The email address of the CC filling the role of RoleName. 
        /// Required when a CC role exists for the Template.
        /// </summary>
        public IDictionary<string, string> Ccs { get; set; }
        public IDictionary<string, SignatureRequestSignerRoleRequest> Signers { get; set; }
        /// <summary>
        /// The value to fill in for custom field with the name of CustomFieldName. 
        /// Required when a CustomField exists in the Template.
        /// </summary>
        public IDictionary<string, object> CustomFields { get; set; }
        /// <summary>
        /// Key-value data that should be attached to the signature request. 
        /// This metadata is included in all API responses and events involving the signature request. 
        /// For example, use the metadata field to store a signer's order number for look up when 
        /// receiving events for the signature request.
        /// </summary>
        /// <remarks>
        /// Each request can include up to 10 metadata keys, with key names up to 
        /// 40 characters long and values up to 500 characters long.
        /// </remarks>
        public IDictionary<string, string> Metadata { get; set; }
        /// <summary>
        /// The client ID of the ApiApp you want to associate with this request.
        /// </summary>
        public string ClientId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            data.AddOptionalField("test_mode", TestMode);

            if (TemplateIds?.Count == 1)
            {
                data.Add("template_id", TemplateIds[0]);
            }
            else if (TemplateIds?.Count > 1)
            {
                data.AddOptionalListField("template_ids", TemplateIds);
            }

            data.AddOptionalField("title", Title);
            data.AddOptionalField("subject", Subject);
            data.AddOptionalField("message", Message);
            data.AddOptionalField("signing_redirect_url", SigningRedirectUrl);

            foreach (KeyValuePair<string, SignatureRequestSignerRoleRequest> signer in Signers)
            {
                data.Add("signers[" + signer.Key + "][name]", signer.Value.Name);
                data.Add("signers[" + signer.Key + "][email_address]", signer.Value.EmailAddress);
            }

            if (Ccs != null)
            {
                foreach (KeyValuePair<string, string> cc in Ccs)
                {
                    data.Add("ccs[" + cc.Key + "][email_address]", cc.Value);
                }
            }

            data.AddOptionalDictionaryField("custom_fields", CustomFields);
            data.AddOptionalDictionaryField("metadata", Metadata);

            return data;
        }
    }
    public class EmbeddedSignatureFromTemplateRequest : IHelloSignRequest
    {
        public int? TestMode { get; set; }
        public string ClientId { get; set; }
        public string TemplateId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SigningRedirectUrl { get; set; }
        public IDictionary<string, string> Ccs { get; set; }
        public IDictionary<string, SignatureRequestSignerRoleRequest> Signers { get; set; }
        public IDictionary<string, string> CustomFields { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            if (TestMode.HasValue)
                data.Add("test_mode", TestMode.Value);

            data.Add("template_id", TemplateId);
            data.Add("client_id", ClientId);
            if (!string.IsNullOrEmpty(Title))
                data.Add("title", Title);

            if (!string.IsNullOrEmpty(Subject))
                data.Add("subject", Subject);

            if (!string.IsNullOrEmpty(Message))
                data.Add("message", Message);

            if (!string.IsNullOrEmpty(SigningRedirectUrl))
                data.Add("signing_redirect_url", SigningRedirectUrl);

            foreach (KeyValuePair<string, SignatureRequestSignerRoleRequest> signer in Signers)
            {
                data.Add("signers[" + signer.Key + "][name]", signer.Value.Name);
                data.Add("signers[" + signer.Key + "][email_address]", signer.Value.EmailAddress);
            }

            if (Ccs != null)
            {
                foreach (KeyValuePair<string, string> cc in Ccs)
                {
                    data.Add("ccs[" + cc.Key + "][email_address]", cc.Value);
                }
            }

            if (CustomFields != null)
            {
                foreach (KeyValuePair<string, string> customField in CustomFields)
                {
                    data.Add("custom_fields[" + customField.Key + "]", customField.Value);
                }
            }

            return data;
        }
    }
    public class SignatureRequestSignerRoleRequest
    {
        /// <summary>
        /// The name of the signer filling the role of RoleName.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The email address of the signer filling the role of RoleName.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The 4- to 12-character access code that will secure this signer's signature page.
        /// </summary>
        public string Pin { get; set; }
    }

    public class SignatureRequestSendReusableFormCcRequest
    {
        public string EmailAddress { get; set; }
    }

    public class SignatureRequestRemindRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId },
                    { "email_address", EmailAddress }
                };
        }
    }

    public class SignatureRequestCancelRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId }
                };
        }
    }

    [Obsolete]
    public class SignatureRequestFinalCopyRequest : IHelloSignStreamCallbackRequest
    {
        public string SignatureRequestId { get; set; }

        public Func<FileResponse, Task> OnResponseStreamAvailable { get; set; }

        public SignatureRequestFinalCopyRequest(Func<FileResponse, Task> onResponseStreamAvailable)
        {
            OnResponseStreamAvailable = onResponseStreamAvailable;
        }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
            {
                { "signature_request_id", SignatureRequestId }
            };
        }
    }

    public class SignatureRequestGetFilesRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }
        public string FileType { get; set; }

        public SignatureRequestGetFilesRequest()
        {
        }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
            {
                { "signature_request_id", SignatureRequestId },
                { "file_type", FileType }
            };
        }
    }

    public class SignatureRequestGetFilesCallbackRequest : SignatureRequestGetFilesRequest, IHelloSignStreamCallbackRequest
    {
        public Func<FileResponse, Task> OnResponseStreamAvailable { get; set; }

        public SignatureRequestGetFilesCallbackRequest(Func<FileResponse, Task> onResponseSteamAvailable)
        {
            OnResponseStreamAvailable = onResponseSteamAvailable;
        }
    }
}
