import React from 'react';
import {MainForm} from '@/widgets/forms/ui/main-form';

export const MainPage: React.FC = () => {
    return (
        <div className="h-full flex items-center justify-center flex-col">
            <MainForm/>
        </div>
    )
}