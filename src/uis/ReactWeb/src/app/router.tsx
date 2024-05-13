import {Route, Routes, useLocation} from 'react-router-dom';
import {LocationState} from '@/shared/types';
import {RoutesMap} from '@/shared/const';
import {ChatPage, HomePage, SignInPage, SignUpPage} from '@/pages/ui';
import {MainPage} from '@/pages/ui/main/page.tsx';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import ManagingPage from '@/pages/ui/managing/page.tsx';
import Page404 from '@/pages/ui/error/page-404.tsx';
import AnimalPage from '@/pages/ui/animal/page.tsx';
import RaidPage from '@/pages/ui/raid/page.tsx';
import FeedingPage from '@/pages/ui/feeding/page.tsx';
import PermissionPage from '@/pages/ui/permission/page.tsx';
import TripPage from '@/pages/ui/trip/page.tsx';
import PaymantPage from '@/pages/ui/paymant/page.tsx';
import HuntingLicensePage from '@/pages/ui/hyntingLicense/page.tsx';
import ProfilePage from '@/pages/ui/profile/page.tsx';
import MyTripsPage from '@/pages/ui/myTrips/page.tsx';
import RentPage from '@/pages/ui/rent/page.tsx';

export const Router = () => {
    const location = useLocation();
    const background = (location.state as LocationState)?.background;
    const {roles} = useAppSelector(selectAuth);

    return(
        <>
            <Routes location={background || location}>
                     <Route path={RoutesMap.home} element={<HomePage />}>
                         <Route index={true} element={<MainPage />}/>
                         {(roles.includes("User")
                          || roles.includes("Manager")
                           || roles.includes("Ranger"))
                           &&  <Route path={RoutesMap.trip} element={<TripPage />}/>
                         }
                         {roles.includes("Admin")
                          && <>
                              <Route path={RoutesMap.managing} element={<ManagingPage />} />
                          </>
                         }
                         {
                             (roles.includes("User")
                                 && <>
                                     <Route path={RoutesMap.paymantFee} element={<PaymantPage />} />
                                     <Route path={RoutesMap.checkHuntingLicense} element={<HuntingLicensePage />} />
                                     <Route path={RoutesMap.rent} element={<RentPage />} />
                                 </>
                             )
                         }
                         {
                             (roles.includes("Ranger")
                                 && <>
                                     <Route path={RoutesMap.raid} element={<RaidPage />} />
                                 </>
                             )
                         }
                         {(roles.includes("Manager") || roles.includes("Ranger"))
                             && <>
                                 <Route path={RoutesMap.feeding} element={<FeedingPage />} />
                                 <Route path={RoutesMap.permission} element={<PermissionPage />} />
                             </>
                         }
                         {roles.includes("Manager")
                             && <>
                                 <Route path={RoutesMap.animal} element={<AnimalPage />} />
                                 <Route path={RoutesMap.rent} element={<RentPage />} />
                             </>
                         }
                         {(roles.includes("Manager") || roles.includes("Director"))
                             && <>
                                 <Route path={RoutesMap.reporting} element={<ManagingPage />} />
                             </>
                         }
                         <Route path={RoutesMap.chat} element={<ChatPage />}/>
                         <Route path={RoutesMap.profile} element={<ProfilePage />}>
                             <Route path={RoutesMap.myTrips} element={<MyTripsPage />}/>
                         </Route>
                     </Route>
                <Route path={RoutesMap.singIn} element={<SignInPage />} />
                <Route path={RoutesMap.singUp} element={<SignUpPage />} />
                <Route path={"*"} element={<Page404/>} />
            </Routes>
        </>
    )
}