import React from 'react';
import {MainForm} from '@/widgets/forms/ui/main-form';

export const MainPage: React.FC = () => {
    return (
        <div className="h-screen flex items-center justify-center flex-col">
            <MainForm/>
        </div>
    )
}