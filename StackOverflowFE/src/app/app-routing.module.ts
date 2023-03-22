import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { HomeComponent } from './home/home.component';
import { QuestionsComponent } from './questions/questions.component';
import { TagsComponent } from './tags/tags.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
    {
        path: '',
        component: AccountComponent,
        loadChildren: () =>
            import('./account/account.module').then((mod) => mod.AccountModule),
    },
    {
        path: 'home',
        component: HomeComponent,
        loadChildren: () =>
            import('./home/home.module').then((mod) => mod.HomeModule),
    },
    {
        path: 'questions',
        component: QuestionsComponent,
        loadChildren: () =>
            import('./questions/questions.module').then(
                (mod) => mod.QuestionsModule,
            ),
    },
    {
        path: 'tags',
        component: TagsComponent,
        loadChildren: () =>
            import('./tags/tags.module').then((mod) => mod.TagsModule),
    },
    {
        path: 'users',
        component: UsersComponent,
        loadChildren: () =>
            import('./users/users.module').then((mod) => mod.UsersModule),
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
