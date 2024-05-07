namespace TasksManager.Domain.DTO
{
    public class ChangePwdDto
    {
        public int idUser {  get; set; }
        public required string oldPwd { get; set; }
        public required string newPwd { get; set; }
    }
}
