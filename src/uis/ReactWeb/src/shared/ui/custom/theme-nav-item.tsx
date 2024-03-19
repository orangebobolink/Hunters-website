import {NavigationMenuItem, navigationMenuRightTriggerStyle, navigationMenuTriggerStyle} from '@/shared/ui';
import {
    DropdownMenu,
    DropdownMenuContent, DropdownMenuItem,
    DropdownMenuTrigger
} from '@/shared/ui/dropdown-menu.tsx';
import {MoonIcon, SunIcon} from '@radix-ui/react-icons';
import {useTheme} from '@/shared/lib/hooks/useTheme.ts';
import {useTranslation} from 'react-i18next';

const ThemeNavItem = () => {
    const {theme, setTheme } = useTheme()
    const { t, i18n } = useTranslation("translation",
        {
            keyPrefix: "header.theme"
        });

    return (
        <NavigationMenuItem className={navigationMenuRightTriggerStyle()}>
            <DropdownMenu>
                <DropdownMenuTrigger asChild>
                    {
                        theme == 'light'
                        ? <SunIcon className="px-2 size-[2.5rem]"/>
                        : <MoonIcon className="px-2 size-[2.5rem]"/>
                    }
                </DropdownMenuTrigger>
                <DropdownMenuContent className="w-9 flex flex-col mr-9">
                    <DropdownMenuItem  onClick={() => setTheme("light")}>
                        {t("light")}
                    </DropdownMenuItem >
                    <DropdownMenuItem  onClick={() => setTheme("dark")}>
                        {t("dark")}
                    </DropdownMenuItem >
                    <DropdownMenuItem  onClick={() => setTheme("system")}>
                        {t("system")}
                    </DropdownMenuItem >
                </DropdownMenuContent>
            </DropdownMenu>
        </NavigationMenuItem>
    );
};

export default ThemeNavItem;