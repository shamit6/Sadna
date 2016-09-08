using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class ParticipantsController : ApiController
    {
        private readonly IParticipantsQueryProcessor _ParticipantsQueryProcessor;

        public ParticipantsController(IParticipantsQueryProcessor ParticipantsQueryProcessor)
        {
            _ParticipantsQueryProcessor = ParticipantsQueryProcessor;
        }

        [HttpGet]
        public DTOs.Participant Get(int id)
        {
            return _ParticipantsQueryProcessor.GetParticipant(id);
        }

        [HttpPost]
        [TransactionFilter]
        public DTOs.Participant Save([FromBody]DTOs.Participant Participant)
        {
            return _ParticipantsQueryProcessor.Save(Participant);
        }

        [HttpPut]
        [TransactionFilter]
        public DTOs.Participant Update([FromUri]int id, [FromBody]DTOs.Participant Participant)
        {
            return _ParticipantsQueryProcessor.Update(id, Participant);
        }

        //[HttpDelete]
        //[TransactionFilter]
        //public void Delete([FromUri]int id)
        //{
        //    _ParticipantsQueryProcessor.Delete(id);
        //}
    }
}