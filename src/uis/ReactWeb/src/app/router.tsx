import {Route, Routes, useLocation } from "react-router-dom";
import {LocationState} from '@/shared/types';
import {RoutesMap} from '@/shared/const';
import {HomePage, SignInPage, SignUpPage} from '@/pages/ui';

export const Router = () => {
    const location = useLocation();
    const background = (location.state as LocationState)?.background;

    return(
        <>
            <Routes location={background || location}>
                <Route path={RoutesMap.home} element={<HomePage />}/>
                <Route path={RoutesMap.singIn} element={<SignInPage />} />
                <Route path={RoutesMap.singUp} element={<SignUpPage />} />
                <Route path={"*"} element={<div>ERROR 404</div>} />
            </Routes>
        </>
    )
}