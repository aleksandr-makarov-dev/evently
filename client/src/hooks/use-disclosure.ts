import { useState, useCallback } from "react";

export function useDisclosure<T = void>(
  defaultIsOpen: boolean = false,
  defaultData?: T
) {
  const [isOpen, setIsOpen] = useState(defaultIsOpen);
  const [data, setData] = useState<T | undefined>(defaultData);

  const open = useCallback((payload?: T) => {
    setIsOpen(true);
    if (payload !== undefined) {
      setData(payload);
    }
  }, []);

  const close = useCallback(() => {
    setIsOpen(false);
    setData(undefined);
  }, []);

  const toggle = useCallback(() => {
    setIsOpen((prev) => !prev);
  }, []);

  return {
    isOpen,
    data,
    open,
    close,
    toggle,
    setIsOpen, // оставляем сеттер на случай кастомной логики
  } as const;
}
