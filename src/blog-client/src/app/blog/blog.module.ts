import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IconDefinition } from '@ant-design/icons-angular';
import { NgZorroAntdModule, NZ_ICON_DEFAULT_TWOTONE_COLOR, NZ_ICONS } from 'ng-zorro-antd';
import { BlogRoutingModule } from './blog-routing.module';
import { BlogAppComponent } from './blog-app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { IconModule } from '@ant-design/icons-angular';


@NgModule({
  declarations: [
    BlogAppComponent,
    SidenavComponent],
  imports: [
    CommonModule,
    BlogRoutingModule,
    NgZorroAntdModule,
    IconModule
  ]
})
export class BlogModule { }
