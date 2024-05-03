import {Route, Routes, useLocation } from "react-router-dom";
import {LocationState} from '@/shared/types';
import {RoutesMap} from '@/shared/const';
import {ChatPage, HomePage, SignInPage, SignUpPage} from '@/pages/ui';
import {MainPage} from '@/pages/ui/main/page.tsx';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import ManagingPage from '@/pages/ui/managing/page.tsx';
import Page404 from '@/pages/ui/error/page-404.tsx';
import Trip from '@/pages/ui/trip/page.tsx';
import AnimalPage from "@/pages/ui/animal/page.tsx";
import RaidPage from '@/pages/ui/raid/page.tsx';
import FeedingPage from '@/pages/ui/feeding/page.tsx';

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
                           &&  <Route path={RoutesMap.trip} element={<Trip />}/>
                         }
                         {roles.includes("Admin")
                          && <>
                              <Route path={RoutesMap.managing} element={<ManagingPage />} />
                          </>
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
                             </>
                         }
                         {roles.includes("Manager")
                             && <>
                                 <Route path={RoutesMap.animal} element={<AnimalPage />} />
                             </>
                         }
                         {(roles.includes("Manager") || roles.includes("Director"))
                             && <>
                                 <Route path={RoutesMap.reporting} element={<ManagingPage />} />
                             </>
                         }
                         <Route path={RoutesMap.chat} element={<ChatPage />}/>
                     </Route>
                <Route path={RoutesMap.singIn} element={<SignInPage />} />
                <Route path={RoutesMap.singUp} element={<SignUpPage />} />
                <Route path={"*"} element={<Page404/>} />
            </Routes>
        </>
    )
}