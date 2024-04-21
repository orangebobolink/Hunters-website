import {Route, Routes, useLocation } from "react-router-dom";
import {LocationState} from '@/shared/types';
import {RoutesMap} from '@/shared/const';
import {ChatPage, HomePage, SignInPage, SignUpPage} from '@/pages/ui';
import {MainPage} from '@/pages/ui/main/page.tsx';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import ManagingPage from '@/pages/ui/managing/page.tsx';

export const Router = () => {
    const location = useLocation();
    const background = (location.state as LocationState)?.background;
    const {roles} = useAppSelector(selectAuth);

    return(
        <>
            <Routes location={background || location}>
                     <Route path={RoutesMap.home} element={<HomePage />}>
                         <Route index={true} element={<MainPage />}/>
                         {roles.includes("User")
                          ? <></>
                          :<></>
                         }
                         {roles.includes("Admin")
                          ? <>
                              <Route path={RoutesMap.managing} element={<ManagingPage />} />
                          </>
                          :<></>
                         }
                         <Route path={RoutesMap.chat} element={<ChatPage />}/>
                     </Route>
                <Route path={RoutesMap.singIn} element={<SignInPage />} />
                <Route path={RoutesMap.singUp} element={<SignUpPage />} />
                <Route path={"*"} element={<div>ERROR 404</div>} />
            </Routes>
        </>
    )
}