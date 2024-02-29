import React from 'react';
import {Head} from '@/widgets/head/head.tsx';
import {Outlet} from 'react-router-dom';

export const HomePage: React.FC = () => {
    return (
        <div className="flex flex-col">
            <Head/>
            <Outlet/>
        </div>
    )
}