import type { QueryConfig } from "@/lib/react-query";
import { queryOptions, useQuery } from "@tanstack/react-query";

export type EventResponse = {
  id: string;
  title: string;
  description: string;
  location: string;
  startsAtUtc: Date;
  endsAtUtc: Date;
  category: { id: string; name: string };
  status: EventStatus;
};

export type EventStatus = "draft" | "published" | "completed" | "cancelled";

const getEvents = async (): Promise<EventResponse[]> => {
  // return api.get("/events");
  return new Promise((resolve) => resolve(mockEvents));
};

export const getEventsQueryOptions = () => {
  return queryOptions({
    queryKey: ["events"],
    queryFn: getEvents,
  });
};

type UseEventsOptions = {
  queryConfig?: QueryConfig<typeof getEventsQueryOptions>;
};

export const useEvents = ({ queryConfig }: UseEventsOptions = {}) => {
  return useQuery({
    ...getEventsQueryOptions(),
    ...queryConfig,
  });
};

export const mockEvents: EventResponse[] = [
  {
    id: "1",
    title: "Фестиваль уличной еды",
    description: "Разнообразие блюд от местных шеф-поваров и фудтраков.",
    location: "Москва, Парк Горького",
    startsAtUtc: new Date("2025-07-15T10:00:00Z"),
    endsAtUtc: new Date("2025-07-15T20:00:00Z"),
    category: { id: "food", name: "Еда" },
    status: "published",
  },
  {
    id: "2",
    title: "Концерт симфонического оркестра",
    description:
      "Исполнение классических произведений Чайковского и Рахманинова.",
    location: "Санкт-Петербург, Филармония",
    startsAtUtc: new Date("2025-06-05T18:30:00Z"),
    endsAtUtc: new Date("2025-06-05T21:00:00Z"),
    category: { id: "music", name: "Музыка" },
    status: "completed",
  },
  {
    id: "3",
    title: "Ярмарка ремёсел",
    description: "Изделия ручной работы, украшения и народные промыслы.",
    location: "Казань, Площадь Тысячелетия",
    startsAtUtc: new Date("2025-08-20T09:00:00Z"),
    endsAtUtc: new Date("2025-08-20T17:00:00Z"),
    category: { id: "market", name: "Ярмарка" },
    status: "draft",
  },
  {
    id: "4",
    title: "Кино под открытым небом",
    description: "Показ классических фильмов в летнем кинотеатре.",
    location: "Сочи, Приморский парк",
    startsAtUtc: new Date("2025-07-01T19:00:00Z"),
    endsAtUtc: new Date("2025-07-01T23:00:00Z"),
    category: { id: "cinema", name: "Кино" },
    status: "published",
  },
  {
    id: "5",
    title: "Велопробег «Здоровый город»",
    description: "Массовый заезд для любителей активного образа жизни.",
    location: "Екатеринбург, Центр города",
    startsAtUtc: new Date("2025-09-10T08:00:00Z"),
    endsAtUtc: new Date("2025-09-10T12:00:00Z"),
    category: { id: "sport", name: "Спорт" },
    status: "cancelled",
  },
  {
    id: "6",
    title: "Выставка современного искусства",
    description: "Картины, инсталляции и цифровое искусство молодых авторов.",
    location: "Новосибирск, Арт-центр",
    startsAtUtc: new Date("2025-10-05T11:00:00Z"),
    endsAtUtc: new Date("2025-10-05T19:00:00Z"),
    category: { id: "art", name: "Искусство" },
    status: "published",
  },
  {
    id: "7",
    title: "Фестиваль воздушных шаров",
    description: "Красочное шоу и полёты на воздушных шарах.",
    location: "Калуга, Набережная",
    startsAtUtc: new Date("2025-08-12T06:00:00Z"),
    endsAtUtc: new Date("2025-08-12T12:00:00Z"),
    category: { id: "festival", name: "Фестиваль" },
    status: "draft",
  },
  {
    id: "8",
    title: "Балет «Лебединое озеро»",
    description:
      "Классический балет П. И. Чайковского в исполнении ведущих артистов.",
    location: "Москва, Большой театр",
    startsAtUtc: new Date("2025-11-02T18:00:00Z"),
    endsAtUtc: new Date("2025-11-02T20:30:00Z"),
    category: { id: "theatre", name: "Театр" },
    status: "published",
  },
  {
    id: "9",
    title: "Фестиваль джаза",
    description: "Выступления джазовых музыкантов со всего мира.",
    location: "Ярославль, набережная Волги",
    startsAtUtc: new Date("2025-07-25T17:00:00Z"),
    endsAtUtc: new Date("2025-07-25T22:00:00Z"),
    category: { id: "music", name: "Музыка" },
    status: "published",
  },
  {
    id: "10",
    title: "Марафон «Бег за мечтой»",
    description:
      "42 км по центральным улицам города с живой музыкой и поддержкой зрителей.",
    location: "Нижний Новгород, пл. Минина",
    startsAtUtc: new Date("2025-09-30T07:00:00Z"),
    endsAtUtc: new Date("2025-09-30T14:00:00Z"),
    category: { id: "sport", name: "Спорт" },
    status: "published",
  },
];
