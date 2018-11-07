using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Transfer
{
    public class ResponseModel : BaseResponse
    {
        public List<CollectionModel> Collecions { get; set; }

        public CollectionModel CollectionModel { get; set; }

        public List<SubseryModel> Subseries { get; set; }
        
        public TranscriptionModel Transcription { get; set; }
        
        public UserModel UserModel { get; set; }
        
        public List<TranscriptionModel> Transcriptions { get;  set; }

        public List<KeywordModel> Keywords { get; set; }

        public List<SubjectModel> Subjects { get; set; }

        public BrowseFormModel BrowseFormModel { get; set; }

        public MainFormModel MainFormModel { get; set; }

        public List<UserModel> Users { get; set; }

    }
}
