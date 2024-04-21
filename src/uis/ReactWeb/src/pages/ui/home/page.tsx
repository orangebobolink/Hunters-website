import React from 'react';
import {Head} from '@/widgets/head/head.tsx';
import {Outlet} from 'react-router-dom';

export const HomePage: React.FC = () => {
    return (
        <div className="flex justify-center h-screen flex-col w-full">
            <Head/>
            <Outlet/>
        </div>
    )
}