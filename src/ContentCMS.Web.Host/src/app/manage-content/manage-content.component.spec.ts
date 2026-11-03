import { Injector } from '@angular/core';
import { of } from 'rxjs';
import { ContentDetailDto, ContentServiceProxy, UpsertContentInput } from '@shared/service-proxies/service-proxies';
import { MenuListReactiveService } from '@shared/reactive/menu-list.reactive.service';
import { ManageContentComponent } from './manage-content.component';

describe('ManageContentComponent save', () => {
  let component: ManageContentComponent;
  let contentService: jasmine.SpyObj<ContentServiceProxy>;
  let menuService: MenuListReactiveService;

  const makeContent = (id: number, pageName: string) =>
    new ContentDetailDto({ id, pageName, pageContent: '<p>Content</p>' });

  beforeEach(() => {
    contentService = jasmine.createSpyObj('ContentServiceProxy', ['insertOrUpdateCMSContent']);
    menuService = new MenuListReactiveService();
    const baseServices = {
      localize: (key: string) => key,
      info: jasmine.createSpy('info')
    };
    const injector = { get: () => baseServices } as Injector;
    component = new ManageContentComponent(injector, contentService, menuService);
    component.contentList = [makeContent(1, 'First page'), makeContent(2, 'Second page')];
  });

  function saveContent(id: number, pageName: string): ContentDetailDto {
    component.content = new UpsertContentInput({ id, pageName, pageContent: '<p>Updated</p>' });
    const saved = new ContentDetailDto(component.content);
    contentService.insertOrUpdateCMSContent.and.returnValue(of(saved));
    component.save();
    return saved;
  }

  it('refreshes the first option after saving its edited content', () => {
    component.contentSelected(1);
    const saved = saveContent(1, 'Updated first page');

    expect(contentService.insertOrUpdateCMSContent).toHaveBeenCalledWith(component.content);
    expect(component.contentList[0]).toEqual(saved);
    expect(component.contentList[1].pageName).toBe('Second page');
    expect(component.contentList.length).toBe(2);
    expect(component.saving).toBe(false);
  });

  it('refreshes an existing option after the first position', () => {
    const saved = saveContent(2, 'Updated second page');

    expect(component.contentList[0].pageName).toBe('First page');
    expect(component.contentList[1]).toEqual(saved);
    expect(component.contentList.length).toBe(2);
  });

  it('does not write a negative array property for an item absent from the list', () => {
    const original = component.contentList.slice();
    saveContent(99, 'Page outside the loaded list');

    expect(component.contentList).toEqual(original);
    expect(Object.prototype.hasOwnProperty.call(component.contentList, '-1')).toBe(false);
  });

  it('adds a newly created page to the list and navigation', () => {
    const saved = makeContent(3, 'New page');
    component.content = new UpsertContentInput({ id: 0, pageName: saved.pageName, pageContent: saved.pageContent });
    contentService.insertOrUpdateCMSContent.and.returnValue(of(saved));

    component.save();

    expect(component.contentList.length).toBe(3);
    expect(component.contentList[2]).toEqual(saved);
    expect(component.content.id).toBe(3);
    expect(menuService.menuItem.value.route).toBe('/app/content/3');
    expect(component.saving).toBe(false);
  });
});
