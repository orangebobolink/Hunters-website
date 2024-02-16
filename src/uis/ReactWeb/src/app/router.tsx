import {Route, Routes, useLocation } from "react-router-dom";
import {LocationState} from '@/shared/types';
import {navigationMap} from '@/shared/const';
import {HomePage} from '@/pages/ui';

export const Router = () => {
    const location = useLocation();
    const background = (location.state as LocationState)?.background;

    return(
        <>
            <Routes location={background || location}>
                <Route path={navigationMap.home} element={<HomePage />} />
                <Route path={"*"} element={<div>404</div>} />
            </Routes>
        </>
    )
}