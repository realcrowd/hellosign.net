using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    /// <summary>
    /// These are the options you can specify for the "type" field.
    /// </summary>
    public static class FieldTypes
    {
        /// <summary>
        /// A text input field
        /// </summary>
        public const string Text = "text";
        /// <summary>
        /// A yes/no checkbox
        /// </summary>
        public const string Checkbox = "checkbox";
        /// <summary>
        /// A date
        /// </summary>
        public const string DateSigned = "date_signed";
        /// <summary>
        /// An input field for initials
        /// </summary>
        public const string Initials = "initials";
        /// <summary>
        /// A signature input field
        /// </summary>
        public const string Signature = "signature";
    }

    /// <summary>
    /// List and details of possible errors that can be returned.
    /// </summary>
    public static class ErrorNames
    {
        public const string BadRequest = "bad_request";
        public const string Unauthorized = "unauthorized";
        public const string PaymentRequired = "payment_required";
        public const string Forbidden = "forbidden";
        public const string NotFound = "not_found";
        public const string Conflict = "conflict";
        public const string TeamInviteFailed = "team_invite_failed";
        public const string InvalidRecipient = "invalid_recipient";
        public const string SignatureRequestCancelFailed = "signature_request_cancel_failed";
        public const string Maintenance = "maintenance";
        public const string Deleted = "deleted";
        public const string Unknown = "unknown";
        public const string MethodNotSupported = "method_not_supported";
        public const string SignatureRequestInvalid = "signature_request_invalid";
        public const string TemplateError = "template_error";
        public const string InvalidReminder = "invalid_reminder";
        public const string ExceededRate = "exceeded_rate";
    }

    /// <summary>
    /// List and details of possible warnings that can be returned.
    /// </summary>
    public static class WarningNames
    {
        public const string Unconfirmed = "unconfirmed";
        public const string CustomFieldValueTooLong = "custom_field_value_too_long";
        public const string CustomFieldValuesTooLong = "custom_field_values_too_long";
        public const string ParameterIgnored = "parameter_ignored";
        public const string NonPdfTextTags = "non_pdf_text_tags";
        public const string FormFieldsOverlap = "form_fields_overlap";
        public const string FormFieldsPlacement = "form_fields_placement";
        public const string DeprecatedParameter = "deprecated_parameter";
        public const string ParameterConflict = "parameter_conflict";
        public const string PartialSuccess = "partial_success";
        public const string TestModeOnly = "test_mode_only";
    }

    /// <summary>
    /// Callback events come along with a SignatureRequest object, under which you can find the list of associated Signatures. 
    /// Each of these Signature object has a status_code field that describes its current state. 
    /// The table below lists all possible codes.
    /// </summary>
    public static class SignatureStatusCodes
    {
        /// <summary>
        /// Success
        /// </summary>
        public const string Success = "success";
        /// <summary>
        /// On hold. This could be because the sending account needs to 
        /// confirm its email address, or because of insufficient funds
        /// </summary>
        public const string OnHold = "on_hold";
        /// <summary>
        /// Signed
        /// </summary>
        public const string Signed = "signed";
        /// <summary>
        /// Awaiting signature
        /// </summary>
        public const string AwaitingSignature = "awaiting_signature";
        /// <summary>
        /// Unknown error
        /// </summary>
        public const string ErrorUnknown = "error_unknown";
        /// <summary>
        /// File could not be converted
        /// </summary>
        public const string ErrorFile = "error_file";
        /// <summary>
        /// Invalid form fields placement
        /// </summary>
        public const string ErrorComponentPosition = "error_component_position";
        /// <summary>
        /// File contained invalid text tags
        /// </summary>
        public const string ErrorTextTag = "error_text_tag";
    }

    public static class AccountRoleCodes
    {
        public const string Admin = "a";
        public const string Member = "m";
    }
     

    /// <summary>
    /// Here is the list of possible events that can be sent to your callbacks.
    /// </summary>
    public static class EventNames
    {
        public const string SignatureRequestViewed = "signature_request_viewed";
        public const string SignatureRequestSigned = "signature_request_signed";
        public const string SignatureRequestSent = "signature_request_sent";
        public const string SignatureRequestRemind = "signature_request_remind";
        public const string SignatureRequestAllSigned = "signature_request_all_signed";
        public const string FileError = "file_error";
        public const string UnknownError = "unknown_error";
        public const string SignatureRequestInvalid = "signature_request_invalid";
        public const string AccountConfirmed = "account_confirmed";
        public const string TemplateCreated = "template_created";
        public const string TemplateError = "template_error";
    }

    public static class FileTypes
    {
        public const string Pdf = "pdf";
        public const string Zip = "zip";
    }

    public static class DataValidationTypes
    {
        /// <summary>
        /// Numbers only (negative and decimal values included)
        /// </summary>
        public const string NumbersOnly = "numbers_only";
        /// <summary>
        /// Letters only (non-English letters included)
        /// </summary>
        public const string LettersOnly = "letters_only";
        /// <summary>
        /// 10- or 11-digit number
        /// </summary>
        public const string PhoneNumber = "phone_number";
        /// <summary>
        /// 9-digit number
        /// </summary>
        public const string BankRoutingNumber = "bank_routing_number";
        /// <summary>
        /// Minimum 6-digit number
        /// </summary>
        public const string BankAccountNumber = "bank_account_number";
        /// <summary>
        /// Email address
        /// </summary>
        public const string EmailAddress = "email_address";
        /// <summary>
        /// 5- or 9-digit number
        /// </summary>
        public const string ZipCode = "zip_code";
        /// <summary>
        /// 9-digit number
        /// </summary>
        public const string SocialSecurityNumber = "social_security_number";
        /// <summary>
        /// 9-digit number
        /// </summary>
        public const string EmployerIdentificationNumber = "employer_identification_number";
    }
}
