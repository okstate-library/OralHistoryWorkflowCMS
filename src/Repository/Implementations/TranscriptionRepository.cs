using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityData;

namespace Repository.Implementations
{
    /// <summary>
    ///
    /// </summary>
    public class TranscriptionRepository :
        GenericRepository<OralCMSDBEntities, transcription>, ITranscriptionRepository
    {

        //public List<Transcription> GetTranscridptions(bool isTranscriptionQueue, List<string> keywords, String searchword)
        //{

        //    var predicate = PredicateBuilder.True<Transcription>();

        //    if (isTranscriptionQueue)
        //    {
        //        predicate = predicate.Or(p => p.TranscriptStatus == false);
        //    }

        //    foreach (string keyword in keywords)
        //    {
        //        if (keyword.Equals("All"))
        //        {
                   
        //        }
        //        else if (keyword.Equals("Priority"))
        //        {
        //            predicate = predicate.Or(p => p.IsPriority == true);
        //        }
        //        else if (keyword.Equals("Transcribed"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("Audit Check"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("First Edit"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("Second Edit"))
        //        {
        //            predicate = predicate.Or(p => string.IsNullOrEmpty(p.SecondEditCompleted));
        //        }
        //        else if (keyword.Equals("Draft Sent"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("Corrections"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("Final Edit"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }
        //        else if (keyword.Equals("Sent out"))
        //        {
        //            predicate = predicate.Or(p => p.Description.Contains(searchword));
        //        }

        //    }

        //    //predicate = predicate.Or(p => p.Description.Contains(searchword));

        //    //var relations = FindBy(predicate);

        //    //// mapping, other stuff, back to business layer
        //    //return relations.ToList();

        //    var dataset = this.FindBy(predicate);

        //    return dataset.ToList();
        //}

        public transcription GetTranscription(int transcriptionId)
        {
            return this.FirstOrDefault(i => i.Id == transcriptionId);
        }
    }
}
