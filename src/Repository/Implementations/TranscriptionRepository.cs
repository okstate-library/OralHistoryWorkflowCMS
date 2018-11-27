using EntityData;

namespace Repository.Implementations
{
    /// <summary>
    ///  Methods define in Transcription Repository
    /// </summary>
    /// <seealso cref="EntityData.GenericRepository{EntityData.OralCMSDBEntities, EntityData.transcription}" />
    /// <seealso cref="Repository.ITranscriptionRepository" />
    public class TranscriptionRepository :
        GenericRepository<OralCMSDBEntities, transcription>, ITranscriptionRepository
    {
        /// <summary>
        /// Gets the transcription.
        /// </summary>
        /// <param name="transcriptionId">The transcription identifier.</param>
        /// <returns>
        /// Returns the specific transcription
        /// </returns>
        public transcription GetTranscription(int transcriptionId)
        {
            return this.FirstOrDefault(i => i.Id == transcriptionId);
        }
    }
}
