import {NavigationMenuItem, navigationMenuTriggerStyle} from '@/shared/ui';
import {
    DropdownMenu,
    DropdownMenuContent, DropdownMenuItem,
    DropdownMenuTrigger
} from '@/shared/ui/dropdown-menu.tsx';
import i18n from 'i18next';

const LanguageNavItem = () => {

    return (
        <NavigationMenuItem className={navigationMenuTriggerStyle()}>
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    <div className="select-none">
                        {
                            i18n.language == "bel"
                            ? "БЕЛ"
                            : "РУС"
                        }
                    </div>
                </DropdownMenuTrigger>
                <DropdownMenuContent className="w-9 flex flex-col mr-9">
                        <DropdownMenuItem
                            onClick={() => i18n.changeLanguage('bel')}>
                            Беларускі
                        </DropdownMenuItem>
                        <DropdownMenuItem
                            onClick={() => i18n.changeLanguage('ru')}>
                            Русский
                        </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </NavigationMenuItem>
    );
};

export default LanguageNavItem;