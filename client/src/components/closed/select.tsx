import { cn } from "@/lib/utils";
import { ChevronDown } from "lucide-react";
import type React from "react";

export type SelectProps = {
  options?: { text: string; value: string; disabled?: boolean }[];
} & React.ComponentProps<"select">;

export const Select = ({ options = [], className, ...props }: SelectProps) => {
  return (
    <div className="relative">
      <select
        className={cn(
          "flex w-full aria-invalid:ring-destructive appearance-none text-base md:text-sm bg-background outline-none px-2 py-1 h-8 ring-input ring-[1px] focus:ring-primary ring-inset rounded disabled:pointer-events-none disabled:opacity-50",
          className
        )}
        {...props}
      >
        <option value="" disabled>
          Не выбрано
        </option>
        {options.map((item) => (
          <option value={item.value} disabled={item.disabled}>
            {item.text}
          </option>
        ))}
      </select>
      <span className="pointer-events-none absolute right-2 top-1/2 -translate-y-1/2">
        <ChevronDown className="h-4 w-4 text-muted-foreground" />
      </span>
    </div>
  );
};
