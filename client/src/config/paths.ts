export const paths = {
  events: {
    root: {
      path: "/events",
    },

    list: {
      path: "/events",
      getHref: () => "/events",
    },

    single: {
      path: ":eventId",
      getHref: (id: string) => `/events/${id}`,
    },

    create: {
      path: "create",
      getHref: () => "/events/create",
    },

    publish: {
      path: ":eventId/publish",
      getHref: (id: string) => `/event/${id}/publish`,
    },
  },
} as const;
