using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;

namespace SchoolDMS.API.Helpers
{
    public static class ValidationHelper
    {
        public static List<DocumentTypeEnum> GetMandatoryDocuments(VisitTypeEnum visitType)
        {
            // Define rules based on visit type. This can be extended.
            var documents = new List<DocumentTypeEnum>();
            
            switch (visitType)
            {
                case VisitTypeEnum.Installation_Demonstration:
                    documents.Add(DocumentTypeEnum.BeforePhoto);
                    documents.Add(DocumentTypeEnum.AfterPhoto);
                    documents.Add(DocumentTypeEnum.SerialNumberImage);
                    documents.Add(DocumentTypeEnum.IR_Certificate);
                    break;
                case VisitTypeEnum.Service_Complaint:
                    documents.Add(DocumentTypeEnum.BeforePhoto);
                    documents.Add(DocumentTypeEnum.AfterPhoto);
                    documents.Add(DocumentTypeEnum.EngineersNotes);
                    break;
                // Add logic for other visit types
                default:
                    documents.Add(DocumentTypeEnum.EngineersNotes);
                    break;
            }

            return documents;
        }

        public static bool HasAllMandatoryDocuments(Visit visit)
        {
            if (visit == null) return false;
            
            var mandatoryTypes = GetMandatoryDocuments(visit.VisitType);
            
            if (visit.Documents == null || !visit.Documents.Any())
            {
                return mandatoryTypes.Count == 0;
            }

            var uploadedTypes = visit.Documents
                                     .Where(d => d.DocumentStatus != DocumentStatusEnum.Rejected)
                                     .Select(d => d.DocumentType)
                                     .ToList();

            foreach (var requiredType in mandatoryTypes)
            {
                if (!uploadedTypes.Contains(requiredType))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
