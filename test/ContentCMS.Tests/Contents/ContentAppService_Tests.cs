using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ContentCMS.Contents;
using ContentCMS.Users;
using Shouldly;
using Xunit;

namespace ContentCMS.Tests.Contents
{
    public class ContentAppService_Tests : ContentCMSTestBase
    {
        private readonly IContentAppService _contentAppService;
        public ContentAppService_Tests()
        {
            _contentAppService = Resolve<IContentAppService>();
        }

        [Fact]
        public async Task Should_Get_Content_List()
        {
            //Act
            var output = await _contentAppService.GetAll();

            //Assert
            output.Items.Count.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
}