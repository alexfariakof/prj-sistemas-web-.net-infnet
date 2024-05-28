﻿using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class AlbumRepository : BaseRepository<Album>, IRepository<Album>
{
    private new RegisterContext Context { get; set; }

    public AlbumRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}