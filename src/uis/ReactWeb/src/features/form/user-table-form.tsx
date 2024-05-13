import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {User} from '@/entities/user/models/User.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {useTranslation} from 'react-i18next';

interface IProps{
    users: User[],
    handleClick: (user:User)=>void
}

const UserTableForm = ({users, handleClick}:IProps) => {
    const { id } = useAppSelector(selectAuth);
    const { t} = useTranslation("translation");

    return (
        <Table>
            <TableHeader>
                <TableRow>
                    <TableHead>{t("registration.firstName")}</TableHead>
                    <TableHead>{t("registration.lastName")}</TableHead>
                    <TableHead>{t("registration.middleName")}</TableHead>
                    <TableHead>{t("registration.email")}</TableHead>
                    <TableHead>{t("registration.roles")}</TableHead>
                    <TableHead>{t("registration.act")}</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {users.map((user) => (
                    user.id !== id &&
                    <TableRow key={user.id}>
                        <TableCell>{user.lastName}</TableCell>
                        <TableCell>{user.firstName}</TableCell>
                        <TableCell>{user.middleName}</TableCell>
                        <TableCell>{user.email}</TableCell>
                        <TableCell>{user.roleNames?.map(role => t(`roles.${role.toLowerCase()}`)).join(", ")}</TableCell>
                        <TableCell>
                            <Button
                                variant="ghost"
                                onClick={() =>handleClick(user)}
                            >
                                {t("managing.edit")}
                            </Button>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
}

export default UserTableForm;