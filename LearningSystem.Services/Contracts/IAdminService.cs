using LearningSystem.Data.ViewModels.Admin;

namespace LearningSystem.Services.Contracts
{
    public interface IAdminService
    {
        SetTrainerModel FindTrainers(int id);

        void SetBlogger(string id);
    }
}
