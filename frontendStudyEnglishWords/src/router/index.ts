import {
  createRouter,
  createWebHistory,
  LocationQuery,
  RouteLocationNormalized,
  RouteRecordName,
  RouteRecordRaw
} from 'vue-router';
import _ from 'lodash';
import AuthService from '@/service/AuthService';

export enum Routes {
  MainView = 'main-view',
  SignInCallback = 'signin-callback',
  SilentCallback = 'silent-callback'
}

export const routes: RouteRecordRaw[] = [
  {
    name: Routes.MainView,
    path: '/',
    component: () => import('@/views/MainView.vue'),
    props: true
  },
  {
    path: `/${Routes.SignInCallback}`,
    component: { template: '' }
  },
  {
    path: `/${Routes.SilentCallback}`,
    component: { template: '' }
  }
];

const Router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior: (to) => {
    if (to.hash) {
      return {
        el: to.hash,
        behavior: 'smooth'
      };
    }
  }
});

const routeQueryStorage = new Map<RouteRecordName, LocationQuery>();

// Router.beforeEach(async (to) => {
//   if (to.path == `/${Routes.SilentCallback}`) {
//     await AuthService.signinSilentCallback();
//     return false;
//   }
// });
//
// Router.beforeEach(
//   async (to: RouteLocationNormalized, from: RouteLocationNormalized, next) => {
//     if (from.name && !_.isEmpty(from.query)) {
//       routeQueryStorage.set(from.name, from.query);
//     }
//
//     if (to.path == `/${Routes.SignInCallback}`) {
//       const user = await AuthService.signinRedirectCallback();
//
//       window.history.replaceState(
//         {},
//         window.document.title,
//         window.location.origin + window.location.pathname
//       );
//
//       next(
//         typeof user.state == 'string'
//           ? user.state.replace(globalThis.location.origin, '')
//           : '/'
//       );
//     } else {
//       const user = await AuthService.getUser();
//       if (user) {
//         if (
//           to.name &&
//           (to.name != from.name || Object.keys(to.query).length == 0) &&
//           Object.keys(to.query).length == 0 &&
//           routeQueryStorage.has(to.name) &&
//           !_.isEqual(to.query, routeQueryStorage.get(to.name))
//         ) {
//           next({ name: to.name, query: routeQueryStorage.get(to.name) });
//         } else {
//           next();
//         }
//       } else {
//         await AuthService.login();
//         return false;
//       }
//     }
//   }
// );

export default Router;
