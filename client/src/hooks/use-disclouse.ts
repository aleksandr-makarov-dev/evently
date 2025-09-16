import { useState } from "react";

export function useDisclouse(defaultValue: boolean = false) {
  const [open, openChange] = useState<boolean>(defaultValue);

  const openDialog = () => openChange(true);
  const closeDialog = () => openChange(false);

  return {
    open,
    openChange,
    openDialog,
    closeDialog,
  };
}
