import React, { useState } from 'react';
import { Permission } from '@/entities/permision/Permision.ts';
import { useTranslation } from 'react-i18next';
import {
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from '@/shared/ui/table.tsx';
import { format } from 'date-fns';
import { Button } from '@/shared/ui';
import PermissionInfoDialog from '@/features/dialog/permission-info-dialog.tsx';
import { EnumService } from '../statusTranslate';
import { ColumnDef } from '@tanstack/react-table';
import { DataTable } from '@/entities/table/ui/data-table';

interface IProps {
    permissions: Permission[];
}

const PermissionDataTable = ({ permissions }: IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedPermission, setSelectedPermission] = useState<Permission>();
    const { t } = useTranslation('translation', {
        keyPrefix: 'feeding.table',
    });

    const columns: ColumnDef<Permission>[] = [
        {
            accessorKey: 'number',
            header: t('number'),
        },
        {
            accessorKey: 'fromDate',
            header: t('fromDate'),
            cell: ({ row }) => {
                const fromDate = new Date(row.getValue('fromDate'));

                return (
                    <div className='text-right font-medium'>
                        {format(fromDate, 'dd.MM.yyyy')}
                    </div>
                );
            },
        },
        {
            accessorKey: 'toDate',
            header: t('toDate'),
            cell: ({ row }) => {
                const fromDate = new Date(row.getValue('toDate'));

                return (
                    <div className='text-right font-medium'>
                        {format(fromDate, 'dd.MM.yyyy')}
                    </div>
                );
            },
        },
        {
            accessorKey: 'animal.name',
            header: t('animalName'),
        },
        {
            accessorKey: 'land.name',
            header: t('location'),
        },
        {
            accessorKey: 'numberOfCoupons',
            header: t('couponLength'),
        },
        {
            accessorKey: 'status',
            header: t('status'),
            cell: ({ row }) => {
                const status = row.getValue('status');

                return (
                    <div className='text-center font-medium'>
                        {EnumService.statysTranslate(status.toString())}
                    </div>
                );
            },
        },
        {
            accessorKey: 'action',
            header: 'Действие',
            cell: ({ row }) => {
                const permission = row.original;

                return (
                    <Button
                        variant='ghost'
                        onClick={() => {
                            setSelectedPermission(permission);
                            setIsOpen(true);
                        }}
                    >
                        {t('moreInf')}
                    </Button>
                );
            },
        },
    ];

    return (
        <>
            <DataTable columns={columns} data={permissions} />
            {selectedPermission && (
                <PermissionInfoDialog
                    permission={selectedPermission}
                    isOpen={isOpen}
                    setIsOpen={setIsOpen}
                />
            )}
        </>
    );
};

export default PermissionDataTable;
