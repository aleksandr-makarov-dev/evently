import { ChevronDown } from "lucide-react";
import type React from "react";
import { InputRoot } from "../ui/input";
import { cn } from "@/lib/utils";

export type SelectProps = {
  placeholder?: string;
  options?: { text: string; value: string; disabled?: boolean }[];
} & React.ComponentProps<"select">;

export const Select = ({
  placeholder = "Не выбрано",
  options = [],
  className,
  ...props
}: SelectProps) => {
  return (
    <InputRoot className={cn("relative p-0", className)}>
      <select
        className="outline-none w-full h-8 px-2 appearance-none"
        {...props}
      >
        <option className="text-muted-foreground" value="" disabled>
          {placeholder}
        </option>
        {options.map((item) => (
          <option key={item.value} value={item.value} disabled={item.disabled}>
            {item.text}
          </option>
        ))}
      </select>
      <span className="pointer-events-none absolute right-2 top-1/2 -translate-y-1/2">
        <ChevronDown className="h-4 w-4 text-muted-foreground" />
      </span>
    </InputRoot>
  );
};
