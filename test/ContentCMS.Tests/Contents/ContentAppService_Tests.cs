using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using Abp.UI;
using ContentCMS.Contents;
using ContentCMS.Contents.Dtos;
using Microsoft.EntityFrameworkCore;
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
        public async Task Should_Create_Content()
        {
            var contentInput = new UpsertContentInput
            {
                PageName = "Test Name",
                PageContent = "<h1>Text Content</h1>"
            };
            
            var newContent = await _contentAppService.InsertOrUpdateCMSContent(contentInput);

            await UsingDbContext(async context =>
            {
                var content = await context.Contents.FindAsync(newContent.Id);

                content.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Update_Content()
        {
            const string newName = "New Test Name";

            var contentInput = new UpsertContentInput
            {
                PageName = "Test Name",
                PageContent = "<h1>Text Content</h1>"
            };

            await UsingDbContext(async context =>
            {
                var newContent = await _contentAppService.InsertOrUpdateCMSContent(contentInput);

                contentInput.Id = newContent.Id;
                contentInput.PageName = newName;
            });


            await _contentAppService.InsertOrUpdateCMSContent(contentInput);

            await UsingDbContext(async context =>
            {
                var content = await context.Contents
                    .FirstOrDefaultAsync(x => x.Id == contentInput.Id && x.PageName == newName);

                content.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Not_Exceed_Max_Limit_For_Page_Content()
        {
            var contentInput = new UpsertContentInput
            {
                PageName = "Test Name",
                PageContent = string.Concat(Enumerable.Repeat("*", Content.MaxContentLength + 1))
            };

            await Should.ThrowAsync<AbpValidationException>(async () =>
            {
                await _contentAppService.InsertOrUpdateCMSContent(contentInput);
            });
        }

        [Fact]
        public async Task Should_Not_Exceed_Max_Limit_For_Page_Name()
        {
            var contentInput = new UpsertContentInput
            {
                PageName = string.Concat(Enumerable.Repeat("*", Content.MaxNameLength + 1)),
                PageContent = "<h1>Text Content</h1>"
            };

            await Should.ThrowAsync<AbpValidationException>(async () =>
            {
                await _contentAppService.InsertOrUpdateCMSContent(contentInput);
            });
        }

        [Fact]
        public async Task Should_Get_Content_List()
        {
            var contentInput1 = new UpsertContentInput
            {
                PageName = "Test Name",
                PageContent = "<h1>Text Content</h1>"
            }; 
            
            var contentInput2 = new UpsertContentInput
            {
                PageName = "Test Name 2",
                PageContent = "<h1>Text Content 2</h1>"
            };

            await _contentAppService.InsertOrUpdateCMSContent(contentInput1);
            await _contentAppService.InsertOrUpdateCMSContent(contentInput2);

            var output = await _contentAppService.GetAll();

            output.Items.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Find_Content()
        {
            var contentInput = new UpsertContentInput
            {
                PageName = "Test Name",
                PageContent = "<h1>Text Content</h1>"
            };

            var newContent = await _contentAppService.InsertOrUpdateCMSContent(contentInput);

            var content = await _contentAppService.GetCMSContent(newContent.Id);

            content.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Throw_Exception_When_Content_Is_Not_Found()
        {
            const int nonExistingId = 1;

            await Should.ThrowAsync<UserFriendlyException>(async () =>
            {
                var content = await _contentAppService.GetCMSContent(nonExistingId);
            });
        }
    }
}