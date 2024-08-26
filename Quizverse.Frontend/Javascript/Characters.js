class Info {
  constructor(count, pages, next, prev) {
      this.Count = count || null;
      this.Pages = pages || null;
      this.Next = next || null;
      this.Prev = prev || null;
  }
}

class Characters {
  constructor(info, results) {
      this.Info = info || null;
      this.Results = results || [];
  }
}

class Location {
  constructor(name, url) {
      this.name = name;
      this.Url = url || null;
  }
}

class Origin {
  constructor(name, url) {
      this.name = name;
      this.Url = url || null;
  }
}

class Character {
  constructor(id, name, status, species, type, gender, origin, location, image, episode, url, created) {
      this.Id = id;
      this.Name = name || null;
      this.Status = status || null;
      this.Species = species || null;
      this.Type = type || null;
      this.Gender = gender || null;
      this.Origin = origin || null;
      this.Location = location || null;
      this.Image = image || null;
      this.Episode = episode || null;
      this.Url = url || null;
      this.Created = created ? new Date(created) : null;
  }
}
