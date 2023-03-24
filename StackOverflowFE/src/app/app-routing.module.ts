import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './account/account.component';
import { AuthGuard } from './core/guards/auth.guard';
import { NotAuthGuard } from './core/guards/not-auth.guard';
import { QuestionsComponent } from './questions/questions.component';

const routes: Routes = [
    {
        path: '',
        component: AccountComponent,
        canActivate: [NotAuthGuard],
        loadChildren: () =>
            import('./account/account.module').then((mod) => mod.AccountModule),
    },
    {
        path: 'home',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./home/home.module').then((mod) => mod.HomeModule),
    },
    {
        path: 'questions',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./questions/questions.module').then(
                (mod) => mod.QuestionsModule,
            ),
    },
    {
        path: 'tags',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./tags/tags.module').then((mod) => mod.TagsModule),
    },
    {
        path: 'users',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./users/users.module').then((mod) => mod.UsersModule),
    },
    {
        path: '**',
        redirectTo: '/home',
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
