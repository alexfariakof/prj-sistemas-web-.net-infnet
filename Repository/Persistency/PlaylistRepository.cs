﻿using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class PlaylistRepository : BaseRepository<Playlist>, IRepository<Playlist>
{
    public RegisterContext Context { get; set; }
    public PlaylistRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}